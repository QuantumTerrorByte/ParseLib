// See https://aka.ms/new-console-template for more information

using NUnit.Framework;
using ParseLib;

namespace ParseLibTests;

[TestFixture]
public class ParserTests
{
    [Test]
    public void NumericCheck()
    {
        var numericHelper = new NumericHelper();

        byte b = 11;
        short s = 22;
        int i = 33;
        long l = 23816293;
        double d = 1.22;
        float f = 222222.222222f;
        decimal dec = 1.22m;

        string str = "";
        object obj = new object();
        object n = null;
        Holder h = new Holder();

        Assert.True(numericHelper.IsNumeric(b.GetType()));
        Assert.True(numericHelper.IsNumeric(s.GetType()));
        Assert.True(numericHelper.IsNumeric(i.GetType()));
        Assert.True(numericHelper.IsNumeric(l.GetType()));
        Assert.True(numericHelper.IsNumeric(d.GetType()));
        Assert.True(numericHelper.IsNumeric(f.GetType()));
        Assert.True(numericHelper.IsNumeric(dec.GetType()));

        Assert.False(numericHelper.IsNumeric(str.GetType()));
        Assert.False(numericHelper.IsNumeric(obj.GetType()));
        Assert.False(numericHelper.IsNumeric(h.GetType()));
    }

    [Test]
    public void NumericParse()
    {
        var numericHelper = new NumericHelper();

        byte b = 11;
        short s = 22;
        int i = 33;
        long l = 23816293;
        double d = 1.22;
        float f = 222222.222222f;
        decimal dec = 1.22m;

        string str = "";
        object obj = new object();
        object n = null;
        Holder h = new Holder();

        Assert.AreEqual(typeof(byte), numericHelper.ParseNumeric(b.GetType(), "22").GetType());
        Assert.AreEqual(typeof(short), numericHelper.ParseNumeric(s.GetType(), "22").GetType());
        Assert.AreEqual(typeof(int), numericHelper.ParseNumeric(i.GetType(), "22").GetType());
        Assert.AreEqual(typeof(long), numericHelper.ParseNumeric(l.GetType(), "22").GetType());
        Assert.AreEqual(typeof(double), numericHelper.ParseNumeric(d.GetType(), "22").GetType());
        Assert.AreEqual(typeof(float), numericHelper.ParseNumeric(f.GetType(), "22").GetType());
        Assert.AreEqual(typeof(decimal), numericHelper.ParseNumeric(dec.GetType(), "22").GetType());

        Assert.Throws<ArgumentException>(() => numericHelper.ParseNumeric(str.GetType(), "22"));
        Assert.Throws<ArgumentException>(() => numericHelper.ParseNumeric(obj.GetType(), "22"));
        Assert.Throws<ArgumentException>(() => numericHelper.ParseNumeric(h.GetType(), "22"));
        
        Assert.Throws<Exception>(() => numericHelper.ParseNumeric(dec.GetType(), ""));
    }


    struct Holder
    {
        public int Value { get; set; }
    }
}