using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Ngol.Utilities.NUnit;
using Ngol.Utilities.System.Extensions;
using Ngol.Utilities.TextFormat.Table;
using NUnit.Framework;

namespace Ngol.Utilities.TextFormat.Table.Tests
{
    [TestFixture]
    public class TestLabeledTableFormatter
    {
        #region Properties

        protected LabeledTableFormatter Formatter { get; set; }

        protected LabeledTableFormatter HytekFormatter { get; set; }

        protected Table HytekTable { get; set; }

        protected Table Table { get; set; }

        #endregion

        #region Set up

        [SetUp]
        public void TestSetUp()
        {
            // Pretty
            Formatter = new PrettyTableFormatter();
            Table = new Table("myTable");
            Table.Columns.Add("thing", typeof(string));
            Table.Columns.Add("stuff", typeof(string));
            Table.Rows.Add("xx", "zzz");
            Table.Rows.Add("walrus", "thingything");
            // Hytek
            HytekFormatter = new LabeledTableFormatter('\0', '|', '\0', '=', '\0', '=', '+');
            HytekTable = new Table();
            HytekTable.Columns.Add(" ", typeof(string));
            HytekTable.Columns.Add("Name", typeof(string));
            HytekTable.Columns.Add("Year", typeof(string));
            HytekTable.Columns.Add("School", typeof(string));
            HytekTable.Columns.Add("Finals", typeof(double), TestTable.FormatTime);
            HytekTable.Columns.Add("Points", typeof(string));
            Assert.AreEqual(6, HytekTable.Columns.Count);
            HytekTable.Rows.Add("1", "Theron Morgan-Brown", "1997", "Lewis & Clark", 1690, "1");
        }

        #endregion

        #region Tests

        [Test]
        public void TestHytekFormat()
        {
            string expected =
@"=+===================+====+=============+========+======
 |Name               |Year|School       |Finals  |Points
=+===================+====+=============+========+======
1|Theron Morgan-Brown|1997|Lewis & Clark|28:10.00|1     ";
            string actual = "\n".Join(HytekFormatter.Format(HytekTable));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestPrettyFormat()
        {
            string expected =
@"+------+-----------+
|thing |stuff      |
+------+-----------+
|xx    |zzz        |
|walrus|thingything|
+------+-----------+";
            string actual = "\n".Join(Formatter.Format(Table));
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
