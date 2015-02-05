using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DishOrderSystemDAL;
using DishOrderSystemDAL.Models;
using System.IO;
using DishOrderSystemBLL;
using DishOrderUtilities;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Test_DishOrderSystem_DAL
{
    [TestClass]
    public class UnitTestDishOrdersystem
    {
        private string _inputString = string.Empty;
        private string _expected = string.Empty;
        private string _outputString = string.Empty;
        private IDishOrderRepository _dishOrderRepository;
        private Mock<IDishOrderModelContainer> _dishOrderServiceModelContainer;
        private DishOrderAdministrator _admin;

        [TestInitialize]
        public void SetUp()
        {
            _dishOrderServiceModelContainer = new Mock<IDishOrderModelContainer>();
            _dishOrderRepository = new DishOrderRepository(_dishOrderServiceModelContainer.Object);
            InitDishOrderModelContainer();
        }


        [TestMethod]
        public void TestCase1() {Assert.IsTrue(Match("morning, 1, 2, 3", "eggs, toast, coffee"));}

        [TestMethod]
        public void TestCase2() { Assert.IsTrue(Match("morning, 2, 1, 3", "eggs, toast, coffee")); }

        [TestMethod]
        public void TestCase3() { Assert.IsTrue(Match("morning, 1, 2, 3, 4", "eggs, toast, coffee, error")); }

        [TestMethod]
        public void TestCase4() { Assert.IsTrue(Match("morning, 1, 2, 3, 3, 3", "eggs, toast, coffee(x3)")); }

        [TestMethod]
        public void TestCase5() { Assert.IsTrue(Match("night, 1, 2, 3, 4", "steak, potato, wine, cake")); }

        [TestMethod]
        public void TestCase6() { Assert.IsTrue(Match("night, 1, 2, 2, 4", "steak, potato(x2), cake")); }

        [TestMethod]
        public void TestCase7() { Assert.IsTrue(Match("night, 1, 2, 3, 5", "steak, potato, wine, error")); }

        [TestMethod]
        public void TestCase8() { Assert.IsTrue(Match("night, 1, 1, 2, 3, 5", "steak, error")); }
        
        private bool Match(string input, string expected)
        {
            bool match = false;
            
            _admin = new DishOrderAdministrator(_dishOrderRepository);
            _outputString = _admin.GetDishOrders(input);
            if (expected == _outputString) match = true;

            return match;
        }

        private void InitDishOrderModelContainer()
        {
            var dishList = new List<Dish> 
            { 
                new Dish { Id = 1, Name = "eggs" },
                new Dish { Id = 2, Name = "toast" },
                new Dish { Id = 3, Name = "coffee" },
                new Dish { Id = 4, Name = "steak" },
                new Dish { Id = 5, Name = "potato" },
                new Dish { Id = 6, Name = "wine" },
                new Dish { Id = 7, Name = "cake" }
            }.AsQueryable();

            var mockDishDbSet = new Mock<IDbSet<Dish>>();
            mockDishDbSet.Setup(m => m.Provider).Returns(dishList.Provider);
            mockDishDbSet.Setup(m => m.Expression).Returns(dishList.Expression);
            mockDishDbSet.Setup(m => m.ElementType).Returns(dishList.ElementType);
            mockDishDbSet.Setup(m => m.GetEnumerator()).Returns(dishList.GetEnumerator());
            _dishOrderServiceModelContainer.SetupGet(x => x.Dishes).Returns(mockDishDbSet.Object);

            var dishTypeList = new List<DishType> 
            { 
                new DishType {Id = 1, Name = "entree"},
                new DishType {Id = 2, Name = "side"},
                new DishType {Id = 3, Name = "drink"},
                new DishType {Id = 4, Name = "dessert"},
            }.AsQueryable();

            var mockDishTypeDbSet = new Mock<IDbSet<DishType>>();
            mockDishTypeDbSet.Setup(m => m.Provider).Returns(dishTypeList.Provider);
            mockDishTypeDbSet.Setup(m => m.Expression).Returns(dishTypeList.Expression);
            mockDishTypeDbSet.Setup(m => m.ElementType).Returns(dishTypeList.ElementType);
            mockDishTypeDbSet.Setup(m => m.GetEnumerator()).Returns(dishTypeList.GetEnumerator());
            _dishOrderServiceModelContainer.SetupGet(x => x.DishTypes).Returns(mockDishTypeDbSet.Object);

            var timeOfDayList = new List<TimeOfDay> 
            { 
                new TimeOfDay {Id = 1, Name = "morning"},
                new TimeOfDay {Id = 2, Name = "night"}
            }.AsQueryable();

            var mockTimeOfDayDbSet = new Mock<IDbSet<TimeOfDay>>();
            mockTimeOfDayDbSet.Setup(m => m.Provider).Returns(timeOfDayList.Provider);
            mockTimeOfDayDbSet.Setup(m => m.Expression).Returns(timeOfDayList.Expression);
            mockTimeOfDayDbSet.Setup(m => m.ElementType).Returns(timeOfDayList.ElementType);
            mockTimeOfDayDbSet.Setup(m => m.GetEnumerator()).Returns(timeOfDayList.GetEnumerator());
            _dishOrderServiceModelContainer.SetupGet(x => x.TimeOfDays).Returns(mockTimeOfDayDbSet.Object);

            var dayTimeDishEntityList = new List<Dish_TimeOfDay> 
            { 
                new Dish_TimeOfDay {Id = 1, TimeOfDay = timeOfDayList.First(x => x.Id == 1), Dish = dishList.First(x => x.Id == 1), EnableMultiple = false},
                new Dish_TimeOfDay {Id = 2, TimeOfDay = timeOfDayList.First(x => x.Id == 1), Dish = dishList.First(x => x.Id == 2), EnableMultiple = false},
                new Dish_TimeOfDay {Id = 3, TimeOfDay = timeOfDayList.First(x => x.Id == 1), Dish = dishList.First(x => x.Id == 3), EnableMultiple = true},
                new Dish_TimeOfDay {Id = 4, TimeOfDay = timeOfDayList.First(x => x.Id == 2), Dish = dishList.First(x => x.Id == 4), EnableMultiple = false},
                new Dish_TimeOfDay {Id = 5, TimeOfDay = timeOfDayList.First(x => x.Id == 2), Dish = dishList.First(x => x.Id == 5), EnableMultiple = true},
                new Dish_TimeOfDay {Id = 6, TimeOfDay = timeOfDayList.First(x => x.Id == 2), Dish = dishList.First(x => x.Id == 6), EnableMultiple = false},
                new Dish_TimeOfDay {Id = 7, TimeOfDay = timeOfDayList.First(x => x.Id == 2), Dish = dishList.First(x => x.Id == 7), EnableMultiple = false}
            }.AsQueryable();

            var mockDish_TimeOfDayDbSet = new Mock<IDbSet<Dish_TimeOfDay>>();
            mockDish_TimeOfDayDbSet.Setup(m => m.Provider).Returns(dayTimeDishEntityList.Provider);
            mockDish_TimeOfDayDbSet.Setup(m => m.Expression).Returns(dayTimeDishEntityList.Expression);
            mockDish_TimeOfDayDbSet.Setup(m => m.ElementType).Returns(dayTimeDishEntityList.ElementType);
            mockDish_TimeOfDayDbSet.Setup(m => m.GetEnumerator()).Returns(dayTimeDishEntityList.GetEnumerator());
            _dishOrderServiceModelContainer.SetupGet(x => x.Dish_TimeOfDay).Returns(mockDish_TimeOfDayDbSet.Object);

            var dish_DishTypeList = new List<Dish_DishType> 
            { 
                new Dish_DishType {Id = 1, DishType = dishTypeList.First(x => x.Id == 1), Dish = dishList.First(x => x.Id == 1)},
                new Dish_DishType {Id = 2, DishType = dishTypeList.First(x => x.Id == 2), Dish = dishList.First(x => x.Id == 2)},
                new Dish_DishType {Id = 3, DishType = dishTypeList.First(x => x.Id == 3), Dish = dishList.First(x => x.Id == 3)},
                new Dish_DishType {Id = 4, DishType = dishTypeList.First(x => x.Id == 1), Dish = dishList.First(x => x.Id == 4)},
                new Dish_DishType {Id = 5, DishType = dishTypeList.First(x => x.Id == 2), Dish = dishList.First(x => x.Id == 5)},
                new Dish_DishType {Id = 6, DishType = dishTypeList.First(x => x.Id == 3), Dish = dishList.First(x => x.Id == 6)},
                new Dish_DishType {Id = 7, DishType = dishTypeList.First(x => x.Id == 4), Dish = dishList.First(x => x.Id == 7)}
            }.AsQueryable();

            var mockDish_DishTypeDbSet = new Mock<IDbSet<Dish_DishType>>();
            mockDish_DishTypeDbSet.Setup(m => m.Provider).Returns(dish_DishTypeList.Provider);
            mockDish_DishTypeDbSet.Setup(m => m.Expression).Returns(dish_DishTypeList.Expression);
            mockDish_DishTypeDbSet.Setup(m => m.ElementType).Returns(dish_DishTypeList.ElementType);
            mockDish_DishTypeDbSet.Setup(m => m.GetEnumerator()).Returns(dish_DishTypeList.GetEnumerator());
            _dishOrderServiceModelContainer.SetupGet(x => x.Dish_DishType).Returns(mockDish_DishTypeDbSet.Object);
        }

    }
}
