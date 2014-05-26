﻿using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class FormulaNodeSensor
    {
        #region Implements IFormulaInterpreter

        public override bool IsNumber()
        {
            return true;
        }

        #endregion
    }

    #region Implementations

    partial class FormulaNodeAccelerationX
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateAccelerationXToken();
        }

        public override double EvaluateNumber()
        {
            return ServiceLocator.SensorService.GetAccelerationX();
        }

        public override string Serialize()
        {
            return AppResources.Formula_Sensor_AccelerationX;
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateAccelerationXNode();
        }
    }

    partial class FormulaNodeAccelerationY
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateAccelerationYToken();
        }

        public override double EvaluateNumber()
        {
            return ServiceLocator.SensorService.GetAccelerationY();
        }

        public override string Serialize()
        {
            return AppResources.Formula_Sensor_AccelerationY;
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateAccelerationYNode();
        }
    }

    partial class FormulaNodeAccelerationZ
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateAccelerationZToken();
        }

        public override double EvaluateNumber()
        {
            return ServiceLocator.SensorService.GetAccelerationZ();
        }

        public override string Serialize()
        {
            return AppResources.Formula_Sensor_AccelerationZ;
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateAccelerationZNode();
        }
    }
    
    partial class FormulaNodeCompass
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateCompassToken();
        }

        public override double EvaluateNumber()
        {
            return ServiceLocator.SensorService.GetCompass();
        }

        public override string Serialize()
        {
            return AppResources.Formula_Sensor_Compass;
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateCompassNode();
        }
    }

    partial class FormulaNodeInclinationX
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateInclinationXToken();
        }

        public override double EvaluateNumber()
        {
            return ServiceLocator.SensorService.GetInclinationX();
        }

        public override string Serialize()
        {
            return AppResources.Formula_Sensor_InclinationX;
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateInclinationXNode();
        }
    }

    partial class FormulaNodeInclinationY
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateInclinationYToken();
        }

        public override double EvaluateNumber()
        {
            return ServiceLocator.SensorService.GetInclinationY();
        }

        public override string Serialize()
        {
            return AppResources.Formula_Sensor_InclinationY;
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateInclinationYNode();
        }
    }

    partial class FormulaNodeLoudness
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateLoudnessToken();
        }

        public override double EvaluateNumber()
        {
            return ServiceLocator.SensorService.GetLoudness();
        }

        public override string Serialize()
        {
            return AppResources.Formula_Sensor_Loudness;
        }

        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateLoudnessNode();
        }
    }

    #endregion
}