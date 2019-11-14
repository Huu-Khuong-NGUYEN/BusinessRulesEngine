using System.Collections.Generic;
using System.ComponentModel;
using BusinessRulesEngine.Interceptors;
using NUnit.Framework;

namespace RuleEngineTests
{
    [TestFixture]
    public class InterfaceWrapperTestFixture
    {
        [Test]
        public void Intercept_changes_with_interface_wrapper()
        {
            {
                var instance = new Abcd();

                var abcd = new InterfaceWrapper<IAbcd>(instance, new AbcdRules(instance));

                var inotify = (INotifyPropertyChanged) abcd;

                var changed = new List<string>();

                inotify.PropertyChanged += (sender, args) => changed.Add(args.PropertyName);

                abcd.Target.A = 1;

                Assert.AreEqual(100, abcd.Target.A);
                Assert.AreEqual(100, instance.A);
                Assert.AreEqual(4, changed.Count);
            }

            {
                var instance = new Bingo();

                var bingo = new InterfaceWrapper<IBingo>(instance, new BingoRules(instance));

                var inotify = (INotifyPropertyChanged) bingo;

                var changed = new List<string>();

                inotify.PropertyChanged += (sender, args) => changed.Add(args.PropertyName);

                bingo.Target.X = 1;

                Assert.AreEqual("BINGO", bingo.Target.Message);
                Assert.AreEqual(101, instance.X);
                Assert.AreEqual(3, changed.Count);

                bingo.Target.Message = "BONGO";
                Assert.AreEqual("BONGO", bingo.Target.Message);
            }
        }
    }
}