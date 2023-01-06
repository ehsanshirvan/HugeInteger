using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugeInteger.Library
{
    public static class Worker
    {
        #region Public Fields

        public delegate ListNode Operate(ListNode num1, ListNode num2);
        #endregion

        #region Properties



        public static Dictionary<char, Operate> Operations = new Dictionary<char, Operate>();
        #endregion

        #region public Methods

        static Worker()
        {
            Operations.Add('+', new Operate(AddTwoNumbers));
            Operations.Add('-', new Operate(SubtractTwoNumbers));
            Operations.Add('*', new Operate(Multiply));

        }

        public static string GetExistOperators()
        {
            return _getExistOperators();
        }

        public static ListNode DoOperate(ListNode num1, ListNode num2, char operatorChar)
        {
            var delOperator = Operations[operatorChar];
            return delOperator.Invoke(num1, num2);
        }

        public static ListNode AddTwoNumbers(ListNode num1, ListNode num2)
        {
            var res = new ListNode();
            var num1LengthIsInRange = _lengthIsValid(num1);
            var num2LengthIsInRange = _lengthIsValid(num2);

            if (num1LengthIsInRange && num2LengthIsInRange)
            {
                var num1ContentIsValid = _validateContent(num1);
                var num2ContentIsValid = _validateContent(num2);

                if (num1ContentIsValid && num2ContentIsValid)
                {
                    var num1Count = _getLength(num1);
                    var num2Count = _getLength(num2);

                    if (num1Count == 0)
                    {
                        res = num2;
                        return res;
                    }

                    if (num2Count == 0)
                    {
                        res = num1;
                        return res;

                    }
                    res = num1Count < num2Count ? _addNumbers(num1, num2) : _addNumbers(num2, num1);

                }

                return res;
            }
            return null;


        }

        public static ListNode SubtractTwoNumbers(ListNode num1, ListNode num2)
        {
            var res = new ListNode();
            var num1LengthIsInRange = _lengthIsValid(num1);
            var num2LengthIsInRange = _lengthIsValid(num2);

            if (num1LengthIsInRange && num2LengthIsInRange)
            {
                var num1ContentIsValid = _validateContent(num1);
                var num2ContentIsValid = _validateContent(num2);

                if (num1ContentIsValid && num2ContentIsValid)
                {
                    var num1Count = _getLength(num1);
                    var num2Count = _getLength(num2);

                    if (num1Count == 0)
                    {
                        res = num2;
                        return res;
                    }

                    if (num2Count == 0)
                    {
                        res = num1;
                        return res;
                    }

                    if (num1 == num2)
                        return res;

                    res = num1 > num2 ? _subtractNumbers(num1, num2) : _subtractNumbers(num2, num1);

                }

                ListNode finalResult = _refineResult(res, num1 < num2);
                return finalResult;
            }
            return null;


        }

        public static ListNode Multiply(ListNode num1, ListNode num2)
        {

            var res = new ListNode();
            var num1LengthIsInRange = _lengthIsValid(num1);
            var num2LengthIsInRange = _lengthIsValid(num2);

            if (num1LengthIsInRange && num2LengthIsInRange)
            {
                var num1ContentIsValid = _validateContent(num1);
                var num2ContentIsValid = _validateContent(num2);

                if (num1ContentIsValid && num2ContentIsValid)
                {
                    var num1Count = _getLength(num1);
                    var num2Count = _getLength(num2);

                    if (num1Count == 0)
                    {
                        res = num2;
                        return res;
                    }

                    if (num2Count == 0)
                    {
                        res = num1;
                        return res;
                    }

                    if (num1 == num2)
                        return res;

                    var num1Str = num1.ToString().Replace(",", "");
                    var num2Str = num2.ToString().Replace(",", "");


                    res = num1 > num2 ? _karatsubaMultiply(num1Str,
                        num2Str) : _karatsubaMultiply(num2Str, num1Str);

                    /*
                     res = num1 > num2 ? _multiply(num1,
                         num2) : _multiply(num2, num1);
                    */
                }

                ListNode finalResult = _refineResult(res, false);
                return finalResult;
            }
            return null;


            //-----------------------------------------



            return res;
        }

        public static ListNode GetNum(string numStr)
        {
            return _getNum(numStr);
        }




        #endregion

        #region Private Methods

        private static ListNode _getNum(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;
            var reversStr = s.Trim().Reverse().ToList();
            try
            {
                var tmp = new ListNode();
                var res = tmp;
                for (int i = 0; i < reversStr.Count; i++)
                {
                    tmp.Val = int.Parse(reversStr[i].ToString());
                    if (i + 1 < reversStr.Count)
                    {
                        var next = new ListNode(int.Parse(reversStr[i + 1].ToString()));
                        tmp.Next = next;
                    }

                    tmp = tmp.Next;
                }

                return res;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private static string _stringAddtion(string a, string b)
        {
            string result = "";
            //a is always the smallest in length
            if (a.Length > b.Length)
            {
                _swap(ref a, ref b);
            }
            a = a.PadLeft(b.Length, '0');
            int length = a.Length;
            int carry = 0, res;
            for (int i = length - 1; i >= 0; i--)
            {
                int num1 = int.Parse(a.Substring(i, 1));
                int num2 = int.Parse(b.Substring(i, 1));
                res = (num1 + num2 + carry) % 10;
                carry = (num1 + num2 + carry) / 10;
                result = result.Insert(0, res.ToString());
            }
            if (carry != 0)
                result = result.Insert(0, carry.ToString());
            return _sanitizeResult(result);
        }


        private static void _swap(ref string num1, ref string num2)
        {
            var tmp = num1;
            num1 = num2;
            num2 = tmp;
        }


        private static string _getExistOperators()
        {
            var operators = Worker.Operations.Aggregate(
                new StringBuilder(), (sb, kv) =>

                    sb.AppendFormat("'{0}' , ", kv.Key), sb => sb.ToString());

            var lastCommaIndex = operators.LastIndexOf(',');
            return operators.Remove(lastCommaIndex);
        }
        private static ListNode _subtractNumbers(ListNode num1, ListNode num2)
        {
            var res = new ListNode();

            var resultTmp = new ListNode();
            res = resultTmp;
            var tmp = new ListNode(0);

            while (num2 != null)
            {
                var tmpVal = num1.Val - num2.Val;

                if (tmpVal < 0 && num1.Next != null)
                {
                    if (num1.Val == 0)
                    {
                        var listNodePointer = num1;
                        listNodePointer = listNodePointer.Next;

                        while (listNodePointer.Val == 0)
                        {
                            listNodePointer.Val = 9;
                            listNodePointer = listNodePointer.Next;
                        }

                        listNodePointer.Val -= 1;
                    }
                    else
                    {
                        if (num1.Next.Val == 0)
                        {
                            var listNodePointer = num1.Next;
                            while (listNodePointer.Val == 0)
                            {
                                listNodePointer.Val = 9;
                                listNodePointer = listNodePointer.Next;
                            }

                            listNodePointer.Val -= 1;
                        }
                        else
                            num1.Next.Val--;
                    }
                    tmpVal += 10;
                }
                resultTmp.Val = tmpVal;

                num1 = num1.Next;
                num2 = num2.Next;
                resultTmp.Next = new ListNode();
                resultTmp = resultTmp.Next;

            }

            while (num1 != null)
            {
                if (resultTmp.Val != null)
                {
                    resultTmp.Next = new ListNode();
                    resultTmp = resultTmp.Next;
                }

                resultTmp.Val = num1.Val;
                num1 = num1.Next;

            }
            return res;
        }

        private static ListNode _refineResult(ListNode data, bool negativeSign)
        {
            ListNode resTemp = new ListNode();
            ListNode tmpSign = resTemp;
            ListNode res = tmpSign;

            if (data.GetDigitCount() == 1)
                return data;
            bool nonZeroNumerSeen = false;
            while (data.Next?.Val != null)
            {
                resTemp.Val = data.Val;
                if (data.Val.Value > 0)
                    nonZeroNumerSeen = true;
                resTemp.Next = new ListNode();
                resTemp = resTemp.Next;
                data = data.Next;
            }

            if (!nonZeroNumerSeen)
            {
                res = new ListNode(0);
                return res;
            }

            if (data.Val != 0)
            {
                resTemp.Next = new ListNode();
                resTemp.Val = data.Val;
            }

            if (negativeSign)
            {
                while (tmpSign.Next?.Val != null)
                {
                    tmpSign = tmpSign.Next;
                }

                tmpSign.Val *= -1;
            }
            return res;
        }


        private static ListNode _addNumbers(ListNode num1, ListNode num2)
        {
            var res = new ListNode();

            var resultTmp = new ListNode();
            res = resultTmp;
            var tmp = new ListNode(0);
            do
            {
                var tmpVal = num1.Val + num2.Val + tmp.Val;
                resultTmp.Val = (tmpVal) % 10;
                tmp.Val = tmpVal >= 10 ? 1 : 0;
                resultTmp.Next = new ListNode();
                num1 = num1.Next;
                num2 = num2.Next;
                resultTmp = resultTmp.Next;
            } while (num1 != null);


            while (num2 != null)
            {
                var tmpVal = num2.Val + tmp.Val;
                resultTmp.Val = (tmpVal) % 10;
                tmp.Val = tmpVal >= 10 ? 1 : 0;
                num2 = num2.Next;
                if (num2 != null || tmp.Val > 0)
                {
                    resultTmp.Next = new ListNode();
                    resultTmp = resultTmp.Next;

                }
            }
            if (tmp.Val > 0)
                resultTmp.Val = tmp.Val;
            return res;
        }

        private static bool _validateContent(ListNode list)
        {
            var res = true;
            var tmpNode = list;
            if (list == null)
                return res;

            while (tmpNode.Next != null)
            {
                tmpNode = tmpNode.Next;
                if (tmpNode.Val >= 0 && tmpNode.Val <= 9)
                    continue;
                res = false;
                break;
            }

            return res;
        }

        private static bool _lengthIsValid(ListNode list)
        {
            var listLength = _getLength(list);
            if (listLength > 100)
                return false;
            return true;
        }

        private static int _getLength(ListNode list)
        {
            var listLength = 0;
            var tmpNode = list;
            if (list != null)
                listLength = 1;
            while (tmpNode?.Next != null)
            {
                ++listLength;
                tmpNode = tmpNode.Next;
            }
            return listLength;

        }

        private static ListNode _multiply(ListNode num1, ListNode num2)
        {
            if (num2.ToString().Length == 1 && num2.Val == 0)
                return new ListNode(0);

            if (num2.ToString().Length == 1 && num2.Val == 1)
                return num1;

            var tmp = new ListNode();
            tmp = num1;
            var res = num1;
            var counter = new ListNode(1);
            while (counter < num2)
            {
                res = res + num1;
                counter += new ListNode(1);
            }

            return res;
        }


        //----------------------------------------

        private static ListNode _karatsubaMultiply(string num1Str, string num2Str)
        {
            var num1 = _getNum(num1Str);
            var num2 = GetNum(num2Str);

            ListNode res = null;
            if (num1Str.Length == 1 || num2Str.Length == 1)
            {

                res = _getNum((long.Parse(num1Str) * long.Parse(num2Str)).ToString());
            }

            else if (num1Str.Length == 0 || num2Str.Length == 0)
                return res;

            else
            {
                int cutPos = _getCutPosition(num1, num2);
                string a = _getFirstPart(num1, cutPos);
                string b = _getSecondPart(num1, cutPos);
                string c = _getFirstPart(num2, cutPos);
                string d = _getSecondPart(num2, cutPos);
                ListNode ac = _karatsubaMultiply(a, c);
                ListNode bd = _karatsubaMultiply(b, d);
                ListNode ab_cd = _karatsubaMultiply(_stringAddtion(a, b), _stringAddtion(c, d));
                res = _getNum(_calculateResult(ac, bd, ab_cd, b.Length + d.Length));
            }

            return res;

        }

        private static int _getCutPosition(string first, string second)
        {
            int min = Math.Min(first.Length, second.Length);
            if (min == 1)
                return 1;
            if (min % 2 == 0)
                return min / 2;
            return min / 2 + 1;
        }

        private static int _getCutPosition(ListNode num1, ListNode num2)
        {

            var first = num1 != null ?
                num1.ToString().Replace(",", "") : string.Empty;
            var second = num2 != null ?
                num2.ToString().Replace(",", "") : string.Empty;
            return _getCutPosition(first, second);
        }

        private static string _calculateResult(ListNode acObj, ListNode bdObj,
            ListNode ab_cdObj, int padding)
        {
            string ac = acObj.ToString().Replace(",", "");
            string bd = bdObj.ToString().Replace(",", "");
            string ab_cd = ab_cdObj.ToString().Replace(",", "");

            string term0 = _stringSubtraction(_stringSubtraction(ab_cd, ac), bd);
            string term1 = term0.PadRight(term0.Length + padding / 2, '0');
            string term2 = ac.PadRight(ac.Length + padding, '0');
            return _stringAddtion(_stringAddtion(term1, term2), bd);
        }

        private static string _stringSubtraction(string a, string b)
        {
            bool resultNegative = false;
            string result = "";
            //a should be the larger number
            if (_stringIsSmaller(a, b))
            {
                _swap(ref a, ref b);
                resultNegative = true;
            }
            b = b.PadLeft(a.Length, '0');
            int length = a.Length;
            int carry = 0, res;
            for (int i = length - 1; i >= 0; i--)
            {
                bool nextCarry = false;
                int num1 = int.Parse(a.Substring(i, 1));
                int num2 = int.Parse(b.Substring(i, 1));
                if (num1 - carry < num2)
                {
                    num1 = num1 + 10;
                    nextCarry = true;
                }
                res = (num1 - num2 - carry);
                result = result.Insert(0, res.ToString());
                if (nextCarry)
                    carry = 1;
                else
                    carry = 0;
            }
            result = _sanitizeResult(result);
            if (resultNegative)
                return result.Insert(0, "-");
            return result;
        }

        private static string _getFirstPart(ListNode num1, int cutPos)
        {
            var str = num1 != null ?
                num1.ToString().Replace(",", "") : string.Empty;

            if (str.Length == 0)
                return str;
            return str.Remove(str.Length - cutPos);
        }

        private static string _getSecondPart(ListNode num1, int cutPos)
        {
            var str = num1 != null ?
                num1.ToString().Replace(",", "") : string.Empty;
            if (str.Length == 0)
                return str;
            return str.Substring(str.Length - cutPos);
        }

        private static string _sanitizeResult(string result)
        {
            result = result.TrimStart(new char[] { '0' });
            if (result.Length == 0)
                result = "0";
            return result;
        }
        private static bool _stringIsSmaller(string a, string b)
        {
            if (a.Length < b.Length)
                return true;
            if (a.Length > b.Length)
                return false;
            char[] arrayA = a.ToCharArray();
            char[] arrayB = b.ToCharArray();
            for (int i = 0; i < arrayA.Length; i++)
            {
                if (arrayA[i] < arrayB[i])
                    return true;
                if (arrayA[i] > arrayB[i])
                    return false;
            }
            return false;
        }
        #endregion
    }
}
