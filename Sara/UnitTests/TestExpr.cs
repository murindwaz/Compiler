﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sara;

namespace UnitTests
{
    [TestClass]
    public class TestExpr
    {
        [TestMethod]
        public void TestExprClass()
        {
            var expr = new Expr(new Num(42), Sara.Type.Int);
            Assert.AreEqual(Tag.NUM, expr.Op.TagValue);
            Assert.AreEqual(Sara.Type.Int, expr.Type);

            Assert.AreSame(expr, expr.Gen());
            Assert.AreSame(expr, expr.Reduce());

            expr.EmitJumps("i < 0", 10, 20);//check from output
            Console.WriteLine();
            expr.Jumping(10, 20);
        }

        [TestMethod]
        public void TestId()
        {
            var id = new Id(new Word("some_var", Tag.ID), Sara.Type.Int, 42);
            Assert.AreEqual(42, id.Offset);
        }

        [TestMethod]
        public void TestTemp()
        {
            var temp = new Temp(Sara.Type.Float);
            var another_temp = new Temp(Sara.Type.Int);
        }

        [TestMethod]
        public void TestOp()
        {
            var plus = new Op(new Token('+'), Sara.Type.Int);
            plus.Reduce(); 

            var mult = new Op(new Token('*'), Sara.Type.Float);
            mult.Reduce();

            var grte = new Op(Word.ge, Sara.Type.Char);
            grte.Reduce();

            var and = new Op(Word.and, Sara.Type.Int);
            and.Reduce();

            //output:
            //Test Name:	TestOp
            //Test Outcome:	Passed
            //Result StandardOutput:	
            //    t1 = +
            //    t2 = *
            //    t3 = >=
            //    t4 = &&
        }

        [TestMethod]
        public void TestArith()
        {
            var add = new Arith(new Token('+'), new Constant(42), new Constant(99));
            Assert.AreEqual("42 + 99", add.ToString());
            Assert.IsTrue(add.Reduce() is Sara.Temp);

            var mult = new Arith(new Token('*'), new Constant(142), new Constant(0));
            var poly = new Arith(new Token('-'), add, mult);
            Assert.AreEqual("42 + 99 - 142 * 0", poly.ToString());
        }

        [TestMethod]
        public void TestUnary()
        {
            var mult = new Arith(new Token('*'), new Constant(42), new Constant(3));
            var u = new Unary(new Token('-'), mult);
            Assert.AreEqual("- 42 * 3", u.ToString());
            Assert.IsTrue(u.Gen() is Expr);
        }
    }
}
