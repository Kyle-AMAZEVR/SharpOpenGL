﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace ShaderCompiler
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using ShaderCompiler;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public partial class MaterialTemplate : MaterialTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("\r\n\r\npublic class ");
            
            #line 9 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ShaderName));
            
            #line default
            #line hidden
            this.Write(" : MaterialBase\r\n{\r\n\tpublic ");
            
            #line 11 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ShaderName));
            
            #line default
            #line hidden
            this.Write("() \r\n\t : base (GetVSSourceCode(), GetFSSourceCode())\r\n\t{\t\r\n\t}\r\n\r\n\tpublic ShaderPr" +
                    "ogram GetProgramObject()\r\n\t{\r\n\t\treturn MaterialProgram;\r\n\t}\r\n\r\n\tpublic void Use(" +
                    ")\r\n\t{\r\n\t\tMaterialProgram.UseProgram();\r\n\t}\r\n\r\n");
            
            #line 26 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
 int index = 0;
foreach(var Sampler in FSProgram.GetSampler2DNames())
{
            
            #line default
            #line hidden
            this.Write("\tpublic void Set");
            
            #line 29 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Sampler));
            
            #line default
            #line hidden
            this.Write("2D(Core.Texture.TextureBase TextureObject)\r\n\t{\r\n\t\tSetTexture(@\"");
            
            #line 31 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Sampler));
            
            #line default
            #line hidden
            this.Write("\", TextureObject);\r\n\t}\r\n\r\n\tpublic void Set");
            
            #line 34 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Sampler));
            
            #line default
            #line hidden
            this.Write("2D(int TextureObject, Sampler sampler)\r\n\t{\r\n\t\tSetTexture(@\"");
            
            #line 36 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Sampler));
            
            #line default
            #line hidden
            this.Write("\", TextureObject);\r\n\t}\r\n\r\n\tpublic TextureBase ");
            
            #line 39 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Sampler.FirstCharToUpper()));
            
            #line default
            #line hidden
            this.Write("2D \r\n\t{\t\r\n\t\tget { return ");
            
            #line 41 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Sampler.ToLower()));
            
            #line default
            #line hidden
            this.Write(";}\r\n\t\tset \r\n\t\t{\t\r\n\t\t\t");
            
            #line 44 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Sampler.ToLower()));
            
            #line default
            #line hidden
            this.Write(" = value;\r\n\t\t\tSetTexture(@\"");
            
            #line 45 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Sampler));
            
            #line default
            #line hidden
            this.Write("\", ");
            
            #line 45 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Sampler.ToLower()));
            
            #line default
            #line hidden
            this.Write(");\t\t\t\r\n\t\t}\r\n\t}\r\n\r\n\tprivate TextureBase ");
            
            #line 49 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Sampler.ToLower()));
            
            #line default
            #line hidden
            this.Write(" = null;\r\n");
            
            #line 50 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
index++;
            
            #line default
            #line hidden
            
            #line 51 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
}
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 53 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
foreach(var pair in VSProgram.GetActiveUniformNameAndTypeList())
            
            #line default
            #line hidden
            
            #line 54 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
{
            
            #line default
            #line hidden
            this.Write("\tpublic ");
            
            #line 55 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pair.Value));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 55 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pair.Key.FirstCharToUpper()));
            
            #line default
            #line hidden
            this.Write("\r\n\t{\r\n\t\tget { return ");
            
            #line 57 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pair.Key.ToLower()));
            
            #line default
            #line hidden
            this.Write("; }\r\n\t\tset \r\n\t\t{\r\n\t\t\t");
            
            #line 60 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pair.Key.ToLower()));
            
            #line default
            #line hidden
            this.Write(" = value;\r\n\t\t\tSetUniformVarData(@\"");
            
            #line 61 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pair.Key));
            
            #line default
            #line hidden
            this.Write("\", ");
            
            #line 61 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pair.Key.ToLower()));
            
            #line default
            #line hidden
            this.Write(");\t\t\t\r\n\t\t}\r\n\t}\r\n\tprivate ");
            
            #line 64 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pair.Value));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 64 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pair.Key.ToLower()));
            
            #line default
            #line hidden
            this.Write(" ;\r\n");
            
            #line 65 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
}
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 67 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
foreach(var pair in FSProgram.GetActiveUniformNameAndTypeList())
            
            #line default
            #line hidden
            
            #line 68 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
{
            
            #line default
            #line hidden
            this.Write("\tpublic ");
            
            #line 69 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pair.Value));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 69 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pair.Key.FirstCharToUpper()));
            
            #line default
            #line hidden
            this.Write("\r\n\t{\r\n\t\tget { return ");
            
            #line 71 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pair.Key.ToLower()));
            
            #line default
            #line hidden
            this.Write("; }\r\n\t\tset \r\n\t\t{\r\n\t\t\t");
            
            #line 74 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pair.Key.ToLower()));
            
            #line default
            #line hidden
            this.Write(" = value;\r\n\t\t\tSetUniformVarData(@\"");
            
            #line 75 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pair.Key));
            
            #line default
            #line hidden
            this.Write("\", ");
            
            #line 75 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pair.Key.ToLower()));
            
            #line default
            #line hidden
            this.Write(");\t\t\t\r\n\t\t}\r\n\t}\r\n\tprivate ");
            
            #line 78 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pair.Value));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 78 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pair.Key.ToLower()));
            
            #line default
            #line hidden
            this.Write(" ;\r\n");
            
            #line 79 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
}
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 81 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
var BlockNameList = new List<string>();
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 83 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
for(int i = 0; i < VSProgram.GetActiveUniformBlockCount(); ++i)
{
    var blockname = VSProgram.GetUniformBlockName(i);
	BlockNameList.Add(blockname);

            
            #line default
            #line hidden
            this.Write("    private ");
            
            #line 88 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 88 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname.ToLower()));
            
            #line default
            #line hidden
            this.Write(" = new ");
            
            #line 88 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname));
            
            #line default
            #line hidden
            this.Write("();\r\n\tpublic ");
            
            #line 89 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 89 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname));
            
            #line default
            #line hidden
            this.Write("\r\n\t{\r\n\t\tget { return ");
            
            #line 91 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname.ToLower()));
            
            #line default
            #line hidden
            this.Write("; }\r\n\t\tset \r\n\t\t{ \r\n\t\t\t");
            
            #line 94 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname.ToLower()));
            
            #line default
            #line hidden
            this.Write(" = value; \r\n\t\t\tthis.SetUniformBufferValue< ");
            
            #line 95 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname));
            
            #line default
            #line hidden
            this.Write(" >(@\"");
            
            #line 95 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname));
            
            #line default
            #line hidden
            this.Write("\", ref value);\r\n\t\t}\r\n\t}\r\n\r\n");
            
            #line 99 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
 var MetaDataList = VSProgram.GetUniformVariableMetaDataList(i).OrderBy(x => x.VariableOffset).ToList();
            
            #line default
            #line hidden
            
            #line 100 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
 foreach(var data in MetaDataList) 
            
            #line default
            #line hidden
            
            #line 101 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
 { 
            
            #line default
            #line hidden
            this.Write("\tpublic ");
            
            #line 102 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.VariableTypeString));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 102 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname));
            
            #line default
            #line hidden
            this.Write("_");
            
            #line 102 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.VariableName.FirstCharToUpper()));
            
            #line default
            #line hidden
            this.Write("\r\n\t{\r\n\t\tget { return ");
            
            #line 104 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname.ToLower()));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 104 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.VariableName));
            
            #line default
            #line hidden
            this.Write(" ; }\r\n\t\tset \r\n\t\t{ \r\n\t\t\t");
            
            #line 107 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname.ToLower()));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 107 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.VariableName));
            
            #line default
            #line hidden
            this.Write(" = value;\r\n\t\t\tthis.SetUniformBufferValue< ");
            
            #line 108 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname));
            
            #line default
            #line hidden
            this.Write(" >(@\"");
            
            #line 108 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname));
            
            #line default
            #line hidden
            this.Write("\", ref ");
            
            #line 108 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname.ToLower()));
            
            #line default
            #line hidden
            this.Write(");\r\n\t\t\t//this.SetUniformBufferMemberValue< ");
            
            #line 109 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.VariableTypeString));
            
            #line default
            #line hidden
            this.Write(" >(@\"");
            
            #line 109 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname));
            
            #line default
            #line hidden
            this.Write("\", ref value, ");
            
            #line 109 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.VariableOffset));
            
            #line default
            #line hidden
            this.Write(" );\r\n\t\t}\r\n\t}\r\n");
            
            #line 112 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 114 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
}
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 116 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
for(int i = 0; i < FSProgram.GetActiveUniformBlockCount(); ++i)
{
    var blockname = FSProgram.GetUniformBlockName(i);
	if(BlockNameList.Contains(blockname))
	{
		continue;
	}

            
            #line default
            #line hidden
            this.Write("    private ");
            
            #line 124 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 124 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname.ToLower()));
            
            #line default
            #line hidden
            this.Write(" = new ");
            
            #line 124 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname));
            
            #line default
            #line hidden
            this.Write("();\r\n\tpublic ");
            
            #line 125 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 125 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname));
            
            #line default
            #line hidden
            this.Write("\r\n\t{\r\n\t\tget { return ");
            
            #line 127 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname.ToLower()));
            
            #line default
            #line hidden
            this.Write("; }\r\n\t\tset \r\n\t\t{ \r\n\t\t\t");
            
            #line 130 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname.ToLower()));
            
            #line default
            #line hidden
            this.Write(" = value; \r\n\t\t\tthis.SetUniformBufferValue< ");
            
            #line 131 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname));
            
            #line default
            #line hidden
            this.Write(" >(@\"");
            
            #line 131 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname));
            
            #line default
            #line hidden
            this.Write("\", ref ");
            
            #line 131 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname.ToLower()));
            
            #line default
            #line hidden
            this.Write(");\r\n\t\t}\r\n\t}\r\n\r\n");
            
            #line 135 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
 var MetaDataList = FSProgram.GetUniformVariableMetaDataList(i).OrderBy(x => x.VariableOffset).ToList();
            
            #line default
            #line hidden
            
            #line 136 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
 foreach(var data in MetaDataList) 
            
            #line default
            #line hidden
            
            #line 137 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
 { 
            
            #line default
            #line hidden
            this.Write("\tpublic ");
            
            #line 138 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.VariableTypeString));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 138 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname));
            
            #line default
            #line hidden
            this.Write("_");
            
            #line 138 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.VariableName.FirstCharToUpper()));
            
            #line default
            #line hidden
            this.Write("\r\n\t{\r\n\t\tget { return ");
            
            #line 140 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname.ToLower()));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 140 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.VariableName));
            
            #line default
            #line hidden
            this.Write(" ; }\r\n\t\tset \r\n\t\t{ \r\n\t\t\t");
            
            #line 143 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname.ToLower()));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 143 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.VariableName));
            
            #line default
            #line hidden
            this.Write(" = value; \r\n\t\t\tthis.SetUniformBufferValue< ");
            
            #line 144 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname));
            
            #line default
            #line hidden
            this.Write(" >(@\"");
            
            #line 144 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname));
            
            #line default
            #line hidden
            this.Write("\", ref ");
            
            #line 144 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname.ToLower()));
            
            #line default
            #line hidden
            this.Write(");\r\n\t\t\t//this.SetUniformBufferMemberValue< ");
            
            #line 145 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.VariableTypeString));
            
            #line default
            #line hidden
            this.Write(" >(@\"");
            
            #line 145 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(blockname));
            
            #line default
            #line hidden
            this.Write("\", ref value, ");
            
            #line 145 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.VariableOffset));
            
            #line default
            #line hidden
            this.Write(" );\r\n\t\t}\r\n\t}\r\n");
            
            #line 148 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 149 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
}
            
            #line default
            #line hidden
            this.Write("\r\n\tpublic static string GetVSSourceCode()\r\n\t{\r\n\t\treturn @\"");
            
            #line 153 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(VSSourceCode));
            
            #line default
            #line hidden
            this.Write("\";\r\n\t}\r\n\r\n\tpublic static string GetFSSourceCode()\r\n\t{\r\n\t\treturn @\"");
            
            #line 158 "C:\MyGitHub\SharpOpenGL\ShaderCodeGenerator\MaterialTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FSSourceCode));
            
            #line default
            #line hidden
            this.Write("\";\r\n\t}\r\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public class MaterialTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
