using Core;
using Core.Buffer;
using Core.CustomEvent;
using Core.CustomSerialize;
using Core.Texture;
using Core.Tickable;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using Core.Asset;
using Core.StaticMesh;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SharpOpenGL.Font;
using SharpOpenGL.PostProcess;
using ZeroFormatter.Formatters;

namespace SharpOpenGL
{
    public class MainWindow : GameWindow
    {
        private GameWindowSettings settings = new GameWindowSettings();
        public MainWindow()
        : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
        }
        
        protected CompiledMaterial.GBufferDraw.ModelTransform ModelMatrix = new CompiledMaterial.GBufferDraw.ModelTransform();
        protected CompiledMaterial.GBufferDraw.CameraTransform Transform = new CompiledMaterial.GBufferDraw.CameraTransform();

        protected Core.MaterialBase.MaterialBase GBufferMaterial = null;
        protected Core.MaterialBase.MaterialBase DefaultMaterial = null;
        protected Core.MaterialBase.MaterialBase GBufferPNCMaterial = null;
        protected Core.MaterialBase.MaterialBase GridMaterial = null;
        protected Core.MaterialBase.MaterialBase ThreeDTextMaterial = null;

        //protected RenderTarget TestRenderTarget= new RenderTarget(1024, 768, 1,PixelInternalFormat.LuminanceAlpha, false);


        protected PostProcess.Skybox SkyboxPostProcess = new Skybox();

        protected Cylinder TestCyliner = new Cylinder(10, 10, 24);
        protected Cone TestCone = new Cone(10, 20, 12);
        protected Arrow TestArrow = new Arrow(10);
        protected ThreeAxis TestAxis = new ThreeAxis();
        protected ThreeDText TestText = null;

        protected StaticMeshAsset Mesh = null;
        protected StaticMeshAsset Sphere = null;
        protected StaticMeshAsset Pistol = null;
        protected Task<StaticMeshAsset> MeshLoadTask = null;
        protected Task<StaticMeshAsset> MeshLoadTask2 = null;
        protected GBuffer MyGBuffer = new GBuffer(1024, 768);

        public event EventHandler<EventArgs> OnResourceCreate;
        public event EventHandler<ScreenResizeEventArgs> OnWindowResize;

        public event EventHandler<KeyboardKeyEventArgs> OnKeyDownEvent;
        public event EventHandler<KeyboardKeyEventArgs> OnKeyUpEvent;

        protected BlitToScreen ScreenBlit = new BlitToScreen();

        protected Texture2D TestTexture = null;

        protected int mainThreadId;

        public int MainThreadId { get { return mainThreadId; } }

        RenderingThread renderingThread = new RenderingThread();

        public int Width
        {
            get => this.ClientSize.X;
        }

        public int Height
        {
            get => this.ClientSize.Y;
        }

        protected override void OnUnload()
        {
            //renderingThread.RequestExit();
            //renderThread.Join();
        }

        protected override void OnLoad()
        {
            /*renderThread = new Thread(renderingThread.Run);
            renderThread.Priority = ThreadPriority.AboveNormal;
            renderThread.Name = "RenderingThread";
            renderThread.Start();*/

            mainThreadId = Thread.CurrentThread.ManagedThreadId;

            OpenGLContext.Get().SetRenderingWindow(this);
            OpenGLContext.Get().SetMainThreadId(MainThreadId);

            Formatter<DefaultResolver, Vector3>.Register(new Vector3Formatter<DefaultResolver>());
            Formatter<DefaultResolver, Vector2>.Register(new Vector2Formatter<DefaultResolver>());
            Formatter<DefaultResolver, Vector4>.Register(new Vector4Formatter<DefaultResolver>());

            VSync = VSyncMode.Off;

            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Cw);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.TextureCubeMap);
            GL.Enable(EnableCap.TextureCubeMapSeamless);

            GL.ClearColor(System.Drawing.Color.DarkGray);

            // register resource create event handler            
            OnResourceCreate += this.ResourceCreate;
            OnResourceCreate += Sampler.OnResourceCreate;



            // resigter window resize event handler
            OnWindowResize += CameraManager.Get().OnWindowResized;

            OnResourceCreate += RenderResource.OnOpenGLContextCreated;
            OnWindowResize += ResizableManager.Get().ResizeEventHandler;

            AssetManager.Get().ImportStaticMeshes();

            OnResourceCreate(this, new EventArgs());

            ScreenBlit.SetGridSize(2, 2);

            OnKeyDownEvent += CameraManager.Get().OnKeyDown;
            OnKeyUpEvent += CameraManager.Get().OnKeyUp;

            OnKeyDownEvent += this.HandleKeyDownEvent;

            Mesh = AssetManager.LoadAssetSync<StaticMeshAsset>("./Resources/Imported/StaticMesh/sponza2.staticmesh");
            Sphere = AssetManager.LoadAssetSync<StaticMeshAsset>("./Resources/Imported/StaticMesh/sphere3.staticmesh");


            GBufferMaterial = ShaderManager.Get().GetMaterial("GBufferDraw");
            DefaultMaterial = ShaderManager.Get().GetMaterial("GBufferWithoutTexture");
            GBufferPNCMaterial = ShaderManager.Get().GetMaterial("GBufferPNC");
            GridMaterial = ShaderManager.Get().GetMaterial("GridRenderMaterial");
            ThreeDTextMaterial = ShaderManager.Get().GetMaterial("ThreeDTextRenderMaterial");

            //FontManager.Get().Initialize();

            TestText = new ThreeDText("Hello World");
        }

        protected void ResourceCreate(object sender, EventArgs e)
        {
        }

        protected void ScreenCaptureGBuffer()
        {
            var colorData = MyGBuffer.GetColorAttachement.GetTexImageAsByte();
            var width = MyGBuffer.GetColorAttachement.Width;
            var height = MyGBuffer.GetColorAttachement.Height;
            FreeImageHelper.SaveAsBmp(ref colorData, width, height, "ColorBuffer.bmp");

            var normalData = MyGBuffer.GetNormalAttachment.GetTexImageAsByte();
            FreeImageHelper.SaveAsBmp(ref normalData, width, height, "NormalBuffer.bmp");
        }

        protected void CaptureStaticMesh()
        {
            StaticMeshCapture.Get().Capture("./Resources/Imported/StaticMesh/pop.staticmesh");
        }

        protected void SwitchCameraMode()
        {
            // 
            CameraManager.Get().SwitchCamera();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            //base.OnUpdateFrame(e);

            if (this.WindowState == OpenTK.Windowing.Common.WindowState.Minimized)
            {
                return;
            }

            //
            MainThreadQueue.Get().Execute();

            TickableObjectManager.Tick(e.Time);

            GL.ClearColor(Color.White);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Transform.View = CameraManager.Get().CurrentCameraView;
            Transform.Proj = CameraManager.Get().CurrentCameraProj;

            // draw cubemap first
            SkyboxPostProcess.Render();

            // 
            MyGBuffer.BindAndExecute(
            () =>
            {
                MyGBuffer.Clear();
            });

            SkyboxPostProcess.GetOutputRenderTarget().Copy(MyGBuffer.GetColorAttachement);

            ScreenBlit.Blit(MyGBuffer.GetColorAttachement, 0, 0, 2, 2);
            //LightPostProcess.Render(MyGBuffer.GetPositionAttachment, MyGBuffer.GetColorAttachement, MyGBuffer.GetNormalAttachment);
            //ScreenBlit.Blit(LightPostProcess.GetOutputRenderTarget().GetColorAttachment0TextureObject(), 0, 0, 2, 2);

            SwapBuffers();
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (OnKeyDownEvent != null) OnKeyDownEvent(this, e);
        }

        public void HandleKeyDownEvent(object sender, KeyboardKeyEventArgs e)
        {
            if (ConsoleCommandManager.Get().IsActive)
            {
                ConsoleCommandManager.Get().OnKeyDown(e);
                if (ConsoleCommandManager.Get().IsActive == false)
                {
                    CameraManager.Get().CurrentCamera.ToggleLock();
                }
                return;
            }

            if (e.Key == Keys.F1)
            {
                CameraManager.Get().SwitchCamera();
            }
            else if (e.Key == Keys.F2)
            {
                CameraManager.Get().CurrentCamera.FOV += OpenTK.Mathematics.MathHelper.DegreesToRadians(1.0f);
            }
            else if (e.Key == Keys.F3)
            {
                CameraManager.Get().CurrentCamera.FOV -= OpenTK.Mathematics.MathHelper.DegreesToRadians(1.0f);
            }
            else if (e.Key == Keys.F5)
            {
                CaptureStaticMesh();
            }
            else if (e.Key == Keys.Apostrophe)
            {
                ConsoleCommandManager.Get().ToggleActive();
                CameraManager.Get().CurrentCamera.ToggleLock();
            }
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (OnKeyUpEvent != null) OnKeyUpEvent(this, e);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            ScreenResizeEventArgs eventArgs = new ScreenResizeEventArgs
            {
                Width = Width,
                Height = Height
            };

            float fAspectRatio = Width / (float)Height;

            OnWindowResize(this, eventArgs);

            Transform.Proj = CameraManager.Get().CurrentCamera.Proj;
            Transform.View = CameraManager.Get().CurrentCamera.View;

            ModelMatrix.Model = Matrix4.CreateScale(1.500f);
            this.Title = string.Format("MyEngine({0}x{1})", Width, Height);
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var renderThread = new Thread(RenderingThread.Get().Run);
            renderThread.Priority = ThreadPriority.AboveNormal;
            renderThread.Name = "RenderingThread";
            renderThread.Start();

            var uiThread = new Thread(UIThread.Get().Run);
            uiThread.Priority = ThreadPriority.Lowest;
            uiThread.Name = "UIThread";
            uiThread.SetApartmentState(ApartmentState.STA);
            uiThread.Start();

            Engine.Get().Initialize();

            while (true)
            {
                if (Engine.Get().IsRequestExit)
                {
                    UIThread.Get().RequestExit();
                    break;
                }
                Engine.Get().Tick();
                Thread.Sleep(1000 / 60);
            }

            renderThread.Join();
            uiThread.Join();
        }
    }
}
