﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ObjectEditor
{
    public class ObjectPropertyDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement elemnt = container as FrameworkElement;
            if (item is Vector4Property)
            {
                return elemnt.FindResource("Vector4Template") as DataTemplate;
            }
            else if (item is Vector3Property)
            {
                return elemnt.FindResource("Vector3Template") as DataTemplate;
            }
            else if (item is Vector2Property)
            {
                return elemnt.FindResource("Vector2Template") as DataTemplate;
            }
            else if (item is FloatProperty)
            {
                return elemnt.FindResource("FloatTemplate") as DataTemplate;
            }
            else if (item is IntProperty)
            {
                return elemnt.FindResource("IntTemplate") as DataTemplate;
            }

            return elemnt.FindResource("Vector3Template") as DataTemplate;
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void AddProperty(string name)
        {
            var label = new Label();
            
        }

        public void SetCameraPosition(string value)
        {
            
        }

       

        public void SetObject(object target)
        {
            var proxy = new ObjectProxy(target);

            ObjPropList.Items.Clear();

            foreach (var item in proxy.PropertyList)
            {
                ObjPropList.Items.Add(item);
            }
            
        }

        private void CreateObjectBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ObjectCreateEventHandler(sender, e);
        }

        public EventHandler<EventArgs> ObjectCreateEventHandler;


        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            var property = textBox.DataContext as ObjectProperty;
            property.ApplyValue();
        }
    }
}