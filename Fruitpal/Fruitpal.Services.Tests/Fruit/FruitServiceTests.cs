using Fruitpal.Common;
using Fruitpal.DataAccess.Fruit;
using Fruitpal.Services.Fruit;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Fruitpal.Services.Tests.Fruit
{
    public class FruitServiceTests
    {
        #region Private Fields

        private readonly Mock<IFruitDao> _fruitDao;

        #endregion Private Fields

        #region Public Constructors

        public FruitServiceTests()
        {
            _fruitDao = new Mock<IFruitDao>(); ;
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public void GetFruitCostEstimates_Input_EmptyFormula_Throws_ArgumentException()
        {
            var fruitData = new List<Dictionary<string, object>>()
            {
                new Dictionary<string,object>()
                {
                    {Constants.Country,"MX"},
                    {Constants.Commodity,"mango"},
                    {Constants.FixedOverhead,"32.00"},
                    {Constants.VariableOverhead,"1.24"}
                },
                new Dictionary<string,object>()
                {
                    {Constants.Country,"BR"},
                    {Constants.Commodity,"mango"},
                    {Constants.FixedOverhead,"32.00"},
                    {Constants.VariableOverhead,"1.24"}
                }
            };

            _fruitDao.Setup(x => x.GetFruitData(It.IsAny<string>())).Returns(fruitData);

            var fruitService = new FruitService(_fruitDao.Object);

            Assert.Throws<ArgumentException>(() => fruitService.GetFruitCostEstimates(string.Empty, string.Empty, 0, 0));
        }

        [Fact]
        public void GetFruitCostEstimates_Input_EmptyFruitName_Throws_ArgumentException()
        {
            _fruitDao.Setup(x => x.GetFruitData(It.IsAny<string>())).Returns(new List<Dictionary<string, object>>());

            var fruitService = new FruitService(_fruitDao.Object);

            Assert.Throws<ArgumentException>(() => fruitService.GetFruitCostEstimates(string.Empty, string.Empty, 0, 0));
        }

        [Fact]
        public void GetFruitCostEstimates_Input_InvalidFormula_Throws_EvaluationException()
        {
            var fruitData = new List<Dictionary<string, object>>()
            {
                new Dictionary<string,object>()
                {
                    {Constants.Country,"MX"},
                    {Constants.Commodity,"mango"},
                    {Constants.FixedOverhead,"32.00"},
                    {Constants.VariableOverhead,"1.24"}
                },
                new Dictionary<string,object>()
                {
                    {Constants.Country,"BR"},
                    {Constants.Commodity,"mango"},
                    {Constants.FixedOverhead,"32.00"},
                    {Constants.VariableOverhead,"1.24"}
                }
            };

            _fruitDao.Setup(x => x.GetFruitData(It.IsAny<string>())).Returns(fruitData);

            var fruitService = new FruitService(_fruitDao.Object);

            Assert.Throws<NCalc.EvaluationException>(() => fruitService.GetFruitCostEstimates("mango", "[fixed_overhead] */ [variable_overhead]", 0, 0));
        }

        [Fact]
        public void GetFruitCostEstimates_Returns_EmptyList()
        {
            var fruitData = new List<Dictionary<string, object>>();

            _fruitDao.Setup(x => x.GetFruitData(It.IsAny<string>())).Returns(fruitData);

            var fruitService = new FruitService(_fruitDao.Object);

            var costEstimates = fruitService.GetFruitCostEstimates("mango", "(([price_per_volume] + [variable_overhead]) * [volume]) + [fixed_overhead]", 0, 0);

            Assert.Empty(costEstimates);
        }

        [Fact]
        public void GetFruitCostEstimates_Returns_Estimates()
        {
            var fruitData = new List<Dictionary<string, object>>()
            {
                new Dictionary<string,object>()
                {
                    {Constants.Country,"MX"},
                    {Constants.Commodity,"mango"},
                    {Constants.FixedOverhead,"32.00"},
                    {Constants.VariableOverhead,"1.24"}
                },
                new Dictionary<string,object>()
                {
                    {Constants.Country,"BR"},
                    {Constants.Commodity,"mango"},
                    {Constants.FixedOverhead,"32.00"},
                    {Constants.VariableOverhead,"1.24"}
                }
            };

            _fruitDao.Setup(x => x.GetFruitData(It.IsAny<string>())).Returns(fruitData);

            var fruitService = new FruitService(_fruitDao.Object);

            var costEstimates = fruitService.GetFruitCostEstimates("mango", "(([price_per_volume] + [variable_overhead]) * [volume]) + [fixed_overhead]", 0, 0);

            Assert.NotEmpty(costEstimates);
        }

        #endregion Public Methods
    }
}