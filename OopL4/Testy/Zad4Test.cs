using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zad4;

namespace Testy
{
    [TestClass]
    public class Zad4Test
    {
        [TestMethod]
        public void TestIndent()
        {            
            var tag = new TagBuilder {IsIntended = true, Indentation = 4};
            tag.StartTag("parent")
                .AddAttribute("parentproperty1", "true")
                .AddAttribute("parentproperty2", "5")
                    .StartTag("child1")
                        .AddAttribute("childproperty1", "c")
                        .AddContent("childbody")
                    .EndTag()
                    .StartTag("child2")
                        .AddAttribute("childproperty2", "c")
                        .AddContent("childbody")
                    .EndTag()
                .EndTag()
                .StartTag("script")
                    .AddContent("$.scriptbody();")
                .EndTag();
            Console.WriteLine(tag.ToString());
            const string indented = @"<parent parentproperty1='true' parentproperty2='5'>
    <child1 childproperty1='c'>
        childbody
    </child1>
    <child2 childproperty2='c'>
        childbody
    </child2>
</parent>
<script>
    $.scriptbody();
</script>";
            Assert.AreEqual(indented, tag.ToString());
        }
    }
}
