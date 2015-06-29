﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dragon;

namespace UnitTests
{
    [TestClass]
    public class TestExpr
    {
        [TestMethod]
        public void TestExprClass()
        {
            var expr = new Expr(new Num(42), Dragon.Type.Int);
            Assert.AreEqual(Tag.NUM, expr.Op.TagValue);
            Assert.AreEqual(Dragon.Type.Int, expr.Type);

            Assert.AreSame(expr, expr.Gen());
            Assert.AreSame(expr, expr.Reduce());

            expr.EmitJumps("i < 0", 10, 20);//check from output
            Console.WriteLine();
            expr.Jumping(10, 20);
        }

        [TestMethod]
        public void TestId()
        {
            var id = new Id(new Word("some_var", Tag.ID), Dragon.Type.Int, 42);
            Assert.AreEqual(42, id.Offset);
        }

        [TestMethod]
        public void TestTemp()
        {
            var temp = new Temp(Dragon.Type.Float);
            Assert.AreEqual(1, temp.Number);            
            Assert.AreEqual("t1", temp.ToString());

            var another_temp = new Temp(Dragon.Type.Int);
            Assert.AreEqual(2, another_temp.Number);
            Assert.AreEqual("t2", another_temp.ToString());
        }
    }
}