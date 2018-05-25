using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieGoals.Data.Helpers
{
    public class Result
    {

        private string _stackTrace;
        private List<string> _errors;
        private List<string> _messages;

        public string StackTrace
        {
            get { return _stackTrace; }
        }

        public object Data { get; set; }

        public List<string> Errors
        {
            get
            {
                if (_errors == null)
                    _errors = new List<string>();
                return _errors;
            }
        }

        public List<string> Messages
        {
            get
            {
                if (_messages == null)
                    _messages = new List<string>();
                return _messages;
            }
        }

        public bool Success
        {
            get { return (_errors == null || _errors.Count == 0); }
        }

        //public void Include(Result result)
        //{
        //    if (result == null)
        //        return;

        //    if (!string.IsNullOrEmpty(result.StackTrace))
        //    {
        //        _stackTrace += result.StackTrace;
        //    }
        //    if (Data == null && result.Data != null)
        //        Data = result.Data;
        //    foreach (string err in result.Errors)
        //    {
        //        Errors.Add(err);
        //    }
        //    foreach (string msg in result.Messages)
        //    {
        //        Messages.Add(msg);
        //    }

        //}

        public void AddException(Exception ex)
        {
            if (string.IsNullOrEmpty(_stackTrace))
                _stackTrace = "";
            _stackTrace += Environment.NewLine + "============================" + Environment.NewLine;
            _stackTrace += Environment.NewLine + ex.Message + Environment.NewLine;
            _stackTrace += Environment.NewLine + "============================" + Environment.NewLine;
            _stackTrace += ex.Source + Environment.NewLine + Environment.NewLine;
            _stackTrace += StackTrace + Environment.NewLine + Environment.NewLine;

            Errors.Add(ex.Message);
        }

        //public bool HasAuthenticationError { get; set; }

        public override string ToString()
        {

            string errorMessage = "";
            foreach (string Err in Errors)
            {
                errorMessage += Err + Environment.NewLine;
            }
            return errorMessage;

        }

    }
}
