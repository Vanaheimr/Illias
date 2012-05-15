
using System.Collections;
using Microsoft.SPOT;

//namespace System.Runtime.CompilerServices
//{
//    [AttributeUsageAttribute(
//      AttributeTargets.Assembly
//      | AttributeTargets.Class
//      | AttributeTargets.Method)]
//    sealed class ExtensionAttribute : Attribute { }
//}

delegate bool Predicate(object o);

sealed class Enumerator : IEnumerator
{
    IEnumerator e;
    Predicate p;

    internal Enumerator(IEnumerator e, Predicate p)
    {
        this.e = e;
        this.p = p;
    }

    object IEnumerator.Current
    {
        get { return e.Current; }
    }

    void IEnumerator.Reset()
    {
        e.Reset();
    }

    bool IEnumerator.MoveNext()
    {
        var b = e.MoveNext();
        while (b && !p(e.Current))
        {
            b = e.MoveNext();
        }
        return b;
    }
}

sealed class Filter : IEnumerable
{
    IEnumerable e;
    Predicate p;

    internal Filter(IEnumerable e, Predicate p)
    {
        this.e = e;
        this.p = p;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return new Enumerator(e.GetEnumerator(), p);
    }
}
