using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MES_Test
{
    [Activity(Label = "Activity_Empty")]
    public class Activity_Empty : Activity
    {

        string fileName;
        string _connectionstring;

        private delegate void DelegateWithNoArgs();

        /// <summary>
        /// The total number of tests that have failed during the previous test run.
        /// </summary>
        private int _failed;

        /// <summary>
        /// If set, then automatically run all the tests and exit.
        /// </summary>
        private bool _autoRun;

        private test.TestCases _test;
        private test.TestCases _testitems;
        TextView tv_welcome;
        List<TextView> _testViews;
        LinearLayout linearLayout_main_empty;
        ScrollView scrollView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Main_Empty);

            // Testcases ... 
            this.scrollView = FindViewById<ScrollView>(Resource.Id.main_empty_scrollview);
            this.linearLayout_main_empty = FindViewById<LinearLayout>(Resource.Id.main_empty);
            tv_welcome = FindViewById<TextView>(Resource.Id.tv_welcome);
            tv_welcome.Click += test_click;

            fileName = Classes.DataInfo_Static.TestDB;
            _connectionstring = String.Format("Data Source={0};Pooling=true;FailIfMissing=false", fileName);

            _failed = 0;
            _autoRun = true;
            _testitems = new test.TestCases();
            //foreach (KeyValuePair<string, bool> pair in _testitems.Tests)
            //{
            //    TextView testView = new TextView(linearLayout_main_empty.Context);
            //    testView.Text = pair.Key;
            //    testView.Click += new EventHandler(_tests_Clicked);
            //    _testViews.Add(testView);
            //}

        }

        public override void OnBackPressed()
        {

            Intent intent = new Intent(this, typeof(MainActivity));
            this.StartActivity(intent);
            this.Finish();

            base.OnBackPressed();
        }

        private void test_click(object sender, EventArgs e)
        {

            try
            {
                string factoryString = "System.Data.SQLite";
                //System.Data.Common.DbProviderFactory factory = System.Data.Common.DbProviderFactories.GetFactory(factoryString);
                System.Data.SQLite.SQLiteFactory factory = new System.Data.SQLite.SQLiteFactory();

                _failed = 0;

                _test = new test.TestCases(factory, _connectionstring);
                _test.Tests = _testitems.Tests;
                _testViews = new List<TextView>();

                _test.OnTestStarting += new test.TestStartingEvent(_test_OnTestStarting);
                _test.OnTestFinished += new test.TestCompletedEvent(_test_OnTestFinished);
                _test.OnAllTestsDone += new EventHandler(_test_OnAllTestsDone);
                //_grid.Rows.Clear();
                //runButton.Enabled = false;

                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(_threadFunc));
                t.IsBackground = true;
                t.Start();

            }
            catch (Exception ex)
            {
                ;
            }        
        }

        private StringBuilder GridToText()
        {
            StringBuilder result = new StringBuilder();

            //foreach (DataGridViewRow row in _grid.Rows)
            foreach (TextView tView in _testViews)
            {
                if (result.Length > 0)
                    result.Append(System.Environment.NewLine);

                //result.AppendFormat("{0}\t{1}\t{2}\t{3}", row.Cells[0].Value,
                //    row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value);
                result.AppendLine(tView.Text);
            }

            return result;
        }

        void _test_OnAllTestsDone(object sender, EventArgs e)
        {
            RunOnUiThread(() =>
            {
                var layoutparams = new LinearLayout.LayoutParams(this.tv_welcome.LayoutParameters);
                layoutparams.SetMargins(0, 0, 0, 10);

                TextView tv = new TextView(this);
                //tv.Tag = args.TestName;
                tv.LayoutParameters = layoutparams;
                tv.Text = "All Tests Done";
                tv.SetBackgroundColor(Android.Graphics.Color.GreenYellow);

                this.linearLayout_main_empty.AddView(tv);
                //this.linearLayout_main_empty.ScrollTo((int)testView.GetX(), (int)testView.GetY());

                _testViews.Add(tv);
                ScrollToEnd();
            });

            //if (InvokeRequired)
            //    Invoke(new EventHandler(_test_OnAllTestsDone), sender, e);
            //else
            //    runButton.Enabled = true;

            //
            // NOTE: In "automatic" mode, check if any of the tests failed and return
            //       the appropriate error code to the operating system as we exit
            //       the process.  Also, attempt to write the entire contents of the
            //       test grid to the standard output channel via the console.  This
            //       may fail if we have no console; however, the failure will simply
            //       be ignored.
            //

            //if (_autoRun)
            //{
            //    try
            //    {
            //        Console.Write("{0}", GridToText());
            //    }
            //    catch
            //    {
            //        // do nothing, ignored.
            //    }

            //    System.Environment.Exit(_failed != 0 ? 1 : 0);
            //}
        }

        private void ScrollToEnd()
        {
            this.scrollView.ScrollY = this.scrollView.Height;
        }

        void _threadFunc()
        {
            _test.Run();
        }

        void _test_OnTestFinished(object sender, test.TestEventArgs args)
        {
            RunOnUiThread(() =>
            {
                try
                {
                    //var testView = _testViews[_testViews.Count - 1];
                    var testView = _testViews.Where(x => ((string)x.Tag) == args.TestName).First();
                    testView.Text = $"{args.ToString()}";
                    if (args.Result == test.TestResultEnum.Failed)
                    {
                        _failed++;
                        testView.SetBackgroundColor(Android.Graphics.Color.Red);
                    }
                }
                catch (Exception ex)
                {
                    ;
                }
            });

            //if (InvokeRequired)
            //    Invoke(new TestCompletedEvent(_test_OnTestFinished), sender, args);
            //else
            //{
            //    _grid.Rows[_grid.Rows.Count - 1].SetValues(args.TestName, args.Result, args.Duration, (args.Exception == null) ? args.Message : args.Exception.Message);
            //    if (args.Result == TestResultEnum.Failed)
            //    {
            //        _failed++;

            //        _grid.Rows[_grid.Rows.Count - 1].Cells[1].Style.BackColor = Color.Red;
            //    }
            //    else if (args.Result == TestResultEnum.Inconclusive)
            //    {
            //        _grid.Rows[_grid.Rows.Count - 1].Cells[1].Style.BackColor = Color.LightBlue;
            //    }
            //    //_grid.Rows[_grid.Rows.Count - 1].Height = _grid.Rows[_grid.Rows.Count - 1].GetPreferredHeight(_grid.Rows.Count - 1, DataGridViewAutoSizeRowMode.AllCells, true);
            //}
        }

        void _test_OnTestStarting(object sender, test.TestEventArgs args)
        {
            RunOnUiThread(() =>
            {
                var layoutparams = new LinearLayout.LayoutParams(this.tv_welcome.LayoutParameters);
                layoutparams.SetMargins(0, 0, 0, 10);

                TextView tv = new TextView(this);                
                tv.Tag = args.TestName;
                tv.LayoutParameters = layoutparams;
                tv.Text = $"{args.TestName}, Starting";

                this.linearLayout_main_empty.AddView(tv);
                //this.linearLayout_main_empty.ScrollTo((int)testView.GetX(), (int)testView.GetY());

                _testViews.Add(tv);
                ScrollToEnd();
            });

            //if (this.InvokeRequired)
            //    Invoke(new TestStartingEvent(_test_OnTestStarting), sender, args);
            //else
            //{
            //    _grid.Rows.Add(args.TestName, "Starting", null, null);
            //    _grid.FirstDisplayedScrollingRowIndex = _grid.Rows.Count - 1;
            //}
        }

    }
}