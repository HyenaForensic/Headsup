using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Headup.Util
{
    class ConvertDic
    {
        public static bool DicToFile(Dictionary<string, Dictionary<string, int>> param, string filePath)
        {
            using (StreamWriter outputFile = new StreamWriter(filePath))
            {
                foreach (KeyValuePair<string, Dictionary<string, int>> subject in param)
                {
                    outputFile.WriteLine("<" + subject.Key + ">");
                    foreach (KeyValuePair<string, int> data in subject.Value)
                    {
                        outputFile.WriteLine(data.Key + "=" + data.Value.ToString());
                    }
                    outputFile.WriteLine();
                }
                outputFile.Close();
            }
            return true;
        }
        public static string DicToString(Dictionary<string, Dictionary<string, string>> param)
        {
            StringBuilder result = new StringBuilder();
            foreach (KeyValuePair<string, Dictionary<string, string>> subject in param)
            {
                result.AppendLine("<" + subject.Key + ">");
                foreach (KeyValuePair<string, string> data in subject.Value)
                {
                    result.AppendLine(data.Key + "=" + data.Value);
                }
                result.AppendLine();
            }
            return result.ToString();
        }
        /// <summary>
        /// Case정보를 로드하기 위해 파일을 딕셔너리로 가져오는 함수
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string, int>> FileToDic(string filePath)
        {
            Dictionary<string, Dictionary<string, int>> result = new Dictionary<string, Dictionary<string, int>>();
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                string subject = null;

                foreach (string show in lines)
                {
                    if (show.StartsWith("<") && show.EndsWith(">")) //subject면
                    {
                        string tmp = show;
                        tmp = tmp.Replace("<", "");
                        tmp = tmp.Replace(">", "");
                        subject = tmp;
                        result.Add(subject, new Dictionary<string, int>());
                    }
                    else if (show.Length != 0 && subject != null && show.Contains("=")) //데이터가 있고 제목이 있고 데이터에 =이 포함되어 있으면
                    {
                        string[] tmp = show.Split('=');
                        result[subject].Add(tmp[0], Int32.Parse(tmp[1]));
                    }
                    else if (show.Length == 0) //데이터가 공백이면 다음 데이터
                    {
                        subject = null; //다음 제목이 들어가야 하기 때문에 초기화를 한다.
                    }
                }
            }
            return result;
        }
    }
}
