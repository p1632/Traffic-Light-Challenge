using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Traffic_Light_Challenge;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            DAOMap myDaoMap = JsonDAOMap.getInstance();

            Map expectedMap = new Map(1, 3, 3, 
                                     new BaseField[3, 3] 
                                        { 
                                          {new Obstacle(), new Street(), new Obstacle()},
                                          {new Obstacle(), new Street(), new Obstacle()},
                                          {new Obstacle(), new Street(), new Obstacle()}
                                        }, 
                                    true);

            Map loadedMap = myDaoMap.LoadMap(1);
            Assert.AreEqual(expectedMap,loadedMap);
        }
    }
}
