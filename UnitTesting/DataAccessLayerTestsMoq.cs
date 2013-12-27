using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVVMWindowsPhone.Core.Portable.DAL;
using UnitTesting.Fakes;
using MVVMWindowsPhone.Core.Portable.Model;
using Microsoft.Phone.Testing;
using Moq;

namespace UnitTesting
{
    /// <summary>
    /// Testexample without 
    /// using Moq, creating 
    /// fakes manually.
    /// </summary>
    [TestClass]
    public class DALTestsMoq
    {

         Mock<IRepository<User, IFakeDBContext<User>>> testRepo;

         List<User> userList;
       
          /// <summary>
        /// Initializes the test.
        /// </summary>
        [TestInitialize]
        public void InitializeTest()
        {

            IQueryable<User> users = null;
                
            userList =  new  List<User>();

            userList.Add(new User() { Image = "http://userimages.com/image1.png", Url = "http://user1.com", UserName = "TestUser1" });
            userList.Add(new User() { Image = "http://userimages.com/image2.png", Url = "http://user2.com", UserName = "TestUser1" });
            userList.Add(new User() { Image = "http://userimages.com/image3.png", Url = "http://user3.com", UserName = "TestUser2" });
            userList.Add(new User() { Image = "http://userimages.com/image4.png", Url = "http://user4.com", UserName = "TestUser3" });
            userList.Add(new User() { Image = "http://userimages.com/image5.png", Url = "http://user5.com", UserName = "TestUser4" });
            userList.Add(new User() { Image = "http://userimages.com/image6.png", Url = "http://user6.com", UserName = "TestUser5" });
            userList.Add(new User() { Image = "http://userimages.com/image7.png", Url = "http://user7.com", UserName = "TestUser6" });
            userList.Add(new User() { Image = "http://userimages.com/image8.png", Url = "http://user8.com", UserName = "TestUser7" });
            userList.Add(new User() { Image = "http://userimages.com/image9.png", Url = "http://user9.com", UserName = "TestUser8" });
            userList.Add(new User() { Image = "http://userimages.com/image10.png", Url = "http://user10.com", UserName = "TestUser9" });
            userList.Add(new User() { Image = "http://userimages.com/image11.png", Url = "http://user11.com", UserName = "TestUser10" });
            userList.Add(new User() { Image = "http://userimages.com/image12.png", Url = "http://user12.com", UserName = "TestUser11" });
            userList.Add(new User() { Image = "http://userimages.com/image13.png", Url = "http://user13.com", UserName = "TestUser12" });
            userList.Add(new User() { Image = "http://userimages.com/image14.png", Url = "http://user14.com", UserName = "TestUser13" });
            userList.Add(new User() { Image = "http://userimages.com/image15.png", Url = "http://user15.com", UserName = "TestUser14" });
            userList.Add(new User() { Image = "http://userimages.com/image16.png", Url = "http://user16.com", UserName = "TestUser15" });
            userList.Add(new User() { Image = "http://userimages.com/image17.png", Url = "http://user17.com", UserName = "TestUser16" });

            users = userList.AsQueryable<User>();

            testRepo = new Mock<IRepository<User, IFakeDBContext<User>>>();

            testRepo.SetupProperty(repo => repo.Driver.Context.FakeTable,userList);

            testRepo.Setup(repo => repo.GetAllEntries()).Returns(users);

            testRepo.Setup(repo => repo.AddEntry(It.IsAny<User>())).Returns((User user) => { 
                userList.Add(user); 
                return user; 
            });

            testRepo.Setup(repo => repo.DeleteEntry(It.IsAny<User>())).Returns((User user) => { userList.Remove(user); return user; });

            testRepo.Setup(repo => repo.GetFilteredEntries(It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>()))
                .Returns(
                (System.Linq.Expressions.Expression<Func<User, bool>> filter) => 
                {
                    return userList.AsQueryable<User>().Where(filter);   
                });

            testRepo.Setup(repo => repo.UpdateEntry(It.IsAny<User>(), It.IsAny<User>())).Returns((User orig, User update) => {


                if (userList.Contains<User>(orig))
                {
                    List<User> list = userList.ToList<User>();
                    list[list.IndexOf(orig)] = update;
                    userList = list;

                    return update;
                }
                else
                {
                    return null;
                }
                    
            });
        }

        /// <summary>
        /// Gets all users and count test.
        /// </summary>
        [TestMethod]
        [Tag("Moq")]
        public void GetAllUsersAndCountTest()
        {

            var allEntries = testRepo.Object.GetAllEntries().Count();
            Assert.AreEqual<int>(allEntries, 17);
        }


        /// <summary>
        /// Adds the user and count test.
        /// </summary>
        [TestMethod]
        [Tag("Moq")]
        public void AddUserAndCountTest()
        {

           
            User userToAdd =  new User() { Image = "Image", Url = "some url", UserName = "TestUser18" };
            var user = testRepo.Object.AddEntry(userToAdd);
            
            Assert.AreEqual<int>(testRepo.Object.GetAllEntries().Count(), 18);

        }

        ///// <summary>
        ///// Users the filter test.
        ///// </summary>
        [TestMethod]
        [Tag("Moq")]
        public void UserFilterTest()
        {

            var filteredUser = testRepo.Object.GetFilteredEntries(user => user.UserName.Contains("TestUser1")).First();

            Assert.AreEqual<string>(filteredUser.UserName,"TestUser1");
        
        }

        ///// <summary>
        ///// Users the delete test.
        ///// </summary>
        [TestMethod]
        [Tag("Moq")]
        public void UserDeleteTest()
        {
            var filteredUser = testRepo.Object.GetFilteredEntries(user => user.UserName.Contains("TestUser1")).First(); 

             var userDeleted = testRepo.Object.DeleteEntry(filteredUser);
            
             Assert.IsNotNull(userDeleted);
        }

        ///// <summary>
        ///// Updates the user test.
        ///// </summary>
        [TestMethod]
        [Tag("Moq")]
        public void UpdateUserTest()
        {
            var userToUpdate = testRepo.Object.GetFilteredEntries(user => user.UserName.Contains("TestUser1")).First();

            var updatedEntry = new User() { UserName = userToUpdate.UserName, Image = "changed", Url = userToUpdate.Url };

            testRepo.Object.Driver.Context.UpdateEntry(userToUpdate, updatedEntry);

            var updatedUser = testRepo.Object.GetFilteredEntries(user => user.UserName.Contains("TestUser1")).First();

            Assert.Equals(updatedUser.Image, "changed");

        }
              
    }
}
