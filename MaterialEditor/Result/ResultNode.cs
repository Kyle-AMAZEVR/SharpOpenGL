﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialEditor
{
    public class ResultNode : NodeViewModel
    {
        public ResultNode()
            : base("Material")
        { }

        protected override void CreateInputOutputConnectors()
        {
            base.CreateInputOutputConnectors();

            // input
            InputConnectors.Add(new ConnectorViewModel("Diffuse", ConnectorDataType.ConstantVector3, 0));
            InputConnectors.Add(new ConnectorViewModel("Normal", ConnectorDataType.ConstantVector3, 1));
            InputConnectors.Add(new ConnectorViewModel("Metallic", ConnectorDataType.ConstantFloat, 2));
            InputConnectors.Add(new ConnectorViewModel("Roughness", ConnectorDataType.ConstantFloat, 3));
        }

        public string GetDiffuseColorCode()
        {
            return GetExpressionForInput(0);
        }

        public string GetNormalColorCode()
        {   
            return GetExpressionForInput(1);            
        }

        public string GetMetallicCode()
        {
            return GetExpressionForInput(2);
        }

        public string GetRoughnessCode()
        {
            return GetExpressionForInput(3);
        }

    }
}
