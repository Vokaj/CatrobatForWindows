﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Resources.Localization;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class FormulaNodeBinaryFunction
    {
        #region Implements IFormulaTokenizer

        public override IEnumerable<IFormulaToken> Tokenize()
        {
            return Enumerable.Repeat(CreateToken(), 1)
                .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(true), 1))
                .Concat(FirstChild == null ? FormulaTokenizer.EmptyChild : FirstChild.Tokenize())
                .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParameterSeparatorToken(), 1))
                .Concat(SecondChild == null ? FormulaTokenizer.EmptyChild : SecondChild.Tokenize())
                .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(false), 1));

        }

        #endregion

        #region Implements IFormulaSerialization

        internal override void Serialize(StringBuilder sb)
        {
            SerializeToken(sb);
            sb.Append("(");
            var firstChild = FirstChild as BaseFormulaTree;
            if (firstChild == null) sb.Append(FormulaSerializer.EmptyChild); else firstChild.Serialize(sb);
            sb.Append(", ");
            var secondChild = SecondChild as BaseFormulaTree;
            if (secondChild == null) sb.Append(FormulaSerializer.EmptyChild); else secondChild.Serialize(sb);
            sb.Append(")");
        }

        #endregion

        #region Implements IFormulaInterpreter

        public override bool IsNumber()
        {
            return IsNumberN2N();
        }

        #endregion
    }

    #region Implementations

    partial class FormulaNodeMin
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateMinToken();
        }

        public override double EvaluateNumber()
        {
            return Math.Min(FirstChild.EvaluateNumber(), SecondChild.EvaluateNumber());
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            sb.Append(AppResources.Formula_Function_Min);
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree firstChild, XmlFormulaTree secondChild)
        {
            return XmlFormulaTreeFactory.CreateMinNode(firstChild, secondChild);
        }
    }

    partial class FormulaNodeMax
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateMaxToken();
        }

        public override double EvaluateNumber()
        {
            return Math.Max(FirstChild.EvaluateNumber(), SecondChild.EvaluateNumber());
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            sb.Append(AppResources.Formula_Function_Max);
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree firstChild, XmlFormulaTree secondChild)
        {
            return XmlFormulaTreeFactory.CreateMaxNode(firstChild, secondChild);
        }
    }

    partial class FormulaNodeRandom
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateRandomToken();
        }

        private static Random _random;
        protected static Random Random
        {
            get
            {
                if (_random == null)
                {
                    _random = new Random();
                }
                return _random;
            }
        }

        public override bool IsNumber()
        {
            return IsNumberT2T();
        }

        public override double EvaluateNumber()
        {
            var from = FirstChild.EvaluateNumber();
            var to = SecondChild.EvaluateNumber();
            return from + Random.NextDouble()*(to - from);
        }

        public override bool EvaluateLogic()
        {
            var from = FirstChild.EvaluateLogic();
            var to = SecondChild.EvaluateLogic();
            return from == to ? from : Random.NextBool();
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            sb.Append(AppResources.Formula_Function_Random);
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree firstChild, XmlFormulaTree secondChild)
        {
            return XmlFormulaTreeFactory.CreateRandomNode(firstChild, secondChild);
        }
    }

    #endregion
}
