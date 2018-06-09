using System;
using System.Collections.Generic;
using DI.Reminder.BL.CachedRepository;
using DI.Reminder.Common.CategoryModel;
using DI.Reminder.Data.CategoryDataBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace DI.Reminder.BL.Tests.Categories
{
    [TestClass]
    public class CategoryStorageTesting
    {
        private Mock<ICacheRepository> _cacheRepository;
        private Mock<ICategoryRepository> _categoryRepository;
        private List<Category> _testingListOfCategories;
        private CategoryStorage.Categories _categories;

        #region TestInitialize
        [TestInitialize]
        public void TestInitialize()
        {
            
            _cacheRepository = new Mock<ICacheRepository>();
            _categoryRepository = new Mock<ICategoryRepository>();
            _categories = new CategoryStorage.Categories(_categoryRepository.Object, _cacheRepository.Object);
            _testingListOfCategories = new List<Category>()
            {
                new Category()
                {
                    ID=1,
                    Name="Important",
                    ParentID=null
                },
                new Category()
                {
                    ID =2,
                    Name="Work",
                    ParentID=1
                },
                new Category()
                {
                    ID=3,
                    Name="Family",
                    ParentID=1
                },
                new Category()
                {
                    ID=4,
                    Name="Other",
                    ParentID=null
                }
            };

        }
        #endregion
        #region GetCategories
        [TestMethod]
        public void GetCategories_InvalidData_Null()
        {
            //
            //Arrange
            //
            int? id = null;
            //
            //Act
            //
            var result = _categories.GetCategories(id);
            //
            //Assert
            //
            Assert.AreEqual(null, result);
            _categoryRepository.Verify(m => m.GetAllCategories(), Times.Never);

        }
        [TestMethod]
        public void GetCategories_ValidData_ListOfCategories()
        {
            //
            //Arrange
            //
            int? id = 1;
            _categoryRepository.Setup(m => m.GetCategories(It.IsAny<int?>())).Returns(_testingListOfCategories.Where(t => t.ParentID == 1).ToList);
            //
            //Act
            //
            var result = _categories.GetCategories(id);
            //
            //Assert
            //
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(_testingListOfCategories[1].Name, result.FirstOrDefault(m => m.ParentID == 1).Name);
            _categoryRepository.Verify(m => m.GetCategories(It.IsAny<int?>()), Times.Once);
        }
        #endregion
    }
}
