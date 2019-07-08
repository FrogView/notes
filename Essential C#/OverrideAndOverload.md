##### C# 重载和重写的区别和示例

> 结论： 基类对象去调用重载方法时，会按照继承关系依次往下查找，如果要调用重写方法，则调用对象必须为重写方法本身所属对象
    
``` C#
static void Main(string[] args)
{
    BaseClass baseClass = new BaseClass();
    //基本函数
    baseClass.Function();

    //基本重载函数
    baseClass = new OverrideClass();
    baseClass.Function();
    OverrideClass overrideClass = new OverrideClass();
    overrideClass.Function();
    //基本重写函数
    baseClass = new OverloadClass();
    baseClass.Function();
    OverloadClass overloadClass = new OverloadClass();
    overloadClass.Function();

    //重载和重写混合时
    baseClass = new OverloadExClass();
    baseClass.Function();
    OverrideClass or = new OverloadExClass();
    or.Function();
    OverloadExClass overloadExClass = new OverloadExClass();
    overloadExClass.Function();

    or = overloadExClass;
    or.Function();

    
    ReadLine();
}
```

``` C#
//类定义
namespace EssentialCSharp
{
    public class BaseClass
    {
        public virtual void Function()
        {
            Console.WriteLine("base");
        }
    }

    public class OverrideClass : BaseClass
    {
        public override void Function()
        {
            Console.WriteLine("override");
        }
    }

    public class OverloadClass : BaseClass
    {
        public new void Function()
        {
            Console.WriteLine("overload");
        }
    }

    public class OverloadExClass : OverrideClass
    {
        public new void Function()
        {
            Console.WriteLine("OverloadEx");
        }
    }

}
```
