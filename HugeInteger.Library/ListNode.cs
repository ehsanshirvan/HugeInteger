using System.Runtime.Serialization.Formatters.Binary;

namespace HugeInteger.Library
{
    [Serializable]
    public class ListNode : IDisposable
    {
        public int? Val;
        public ListNode Next;
        public bool IsPositive => Val >= 0;

        public char Sign
        {
            get
            {
                if (IsPositive) return '+';
                return '-';
            }
        }

        public ListNode(int? val = null, ListNode next = null)
        {
            this.Val = val;
            this.Next = next;
        }


        public void PadLeft(long lengthToPad, char ch)
        {
            var tmp = this;

            while (tmp.Val != null)
            {
                tmp = tmp.Next;
            }
            for (int i = 0; i < lengthToPad; i++)
            {
                tmp.Val = 0;
                tmp.Next = new ListNode(0);
                tmp = tmp.Next;
            }
        }

        public ListNode Subset(int startPoint)
        {
            var tmp = new ListNode();
            var res = tmp;




            return res;

        }

        public static bool operator <(ListNode num1, ListNode num2)
        {
            var num1DigitsCount = num1.GetDigitCount();
            var num2DigitsCount = num2.GetDigitCount();

            if (num1 == num2)
                return false;

            if (num1DigitsCount < num2DigitsCount)
                return true;

            var res = false;
            if (num1DigitsCount == num2DigitsCount)
            {
                var tmpnum1 = num1;
                var tmpnum2 = num2;

                while (tmpnum1 != null && tmpnum2 != null)
                {
                    res = tmpnum1.Val < tmpnum2.Val | res;
                    tmpnum1 = tmpnum1.Next;
                    tmpnum2 = tmpnum2.Next;
                }
            }

            return res;
        }

        public static bool operator >(ListNode num1, ListNode num2)
        {
            var num1DigitsCount = num1.GetDigitCount();
            var num2DigitsCount = num2.GetDigitCount();

            if (num1 == num2)
                return false;

            if (num1DigitsCount > num2DigitsCount)
                return true;

            var res = false;
            if (num1DigitsCount == num2DigitsCount)
            {
                var tmpnum1 = num1;
                var tmpnum2 = num2;

                while (tmpnum1 != null)
                {
                    res = (tmpnum1.Val > tmpnum2.Val) | res;
                    tmpnum1 = tmpnum1.Next;
                    tmpnum2 = tmpnum2.Next;
                }
            }

            return res;
        }




        public static bool operator >=(ListNode num1, ListNode num2)
        {
            var res = num1 > num2 || num1 == num2;
            return res;
        }

        public static bool operator <=(ListNode num1, ListNode num2)
        {
            var res = num1 < num2 || num1 == num2;
            return res;
        }

        #region Methods

        public override string ToString()
        {
            var res = string.Empty;
            var tmp = this;
            var groupSize = 0;
            while (tmp != null)
            {
                groupSize += 1;
                if (groupSize % 3 == 0 && tmp.Next?.Val != null)
                {
                    res = string.Concat(res, tmp.Val, ",");
                    groupSize = 0;
                }
                else
                {
                    res = string.Concat(res, tmp.Val);

                }
                tmp = tmp.Next;
            }

            var finalVal = string.Join("", res.Reverse());
            if (finalVal.Contains("-"))
                finalVal = string.Concat("-", finalVal.Replace("-", ""));
            return finalVal;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public static ListNode operator +(ListNode num1, ListNode num2)

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

                    if (num1Count < num2Count)
                        res = _addNumbers(num1, num2);
                    else
                    {
                        res = _addNumbers(num2, num1);

                    }

                }

                return res;
            }
            else
            {
                return null;
            }

        }

        public static ListNode operator -(ListNode num1, ListNode num2)
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



        public Int64 GetDigitCount()
        {
            return _getLength(this);
        }



        #region Private Methods
        private static ListNode _addNumbers(ListNode num1, ListNode num2)
        {
            var res = new ListNode();

            if (num2.Val == null)
            {
                return num1;
            }

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


            while (num2?.Val != null)
            {
                var data1 = num2.Val ?? 0;
                var tmpVal = data1 + tmp.Val.Value;
                resultTmp.Val = (tmpVal) % 10;
                tmp.Val = tmpVal >= 10 ? 1 : 0;
                num2 = num2.Next;
                if (num2?.Val != null || tmp.Val > 0)
                {
                    resultTmp.Next = new ListNode();
                    resultTmp = resultTmp.Next;
                    resultTmp.Val = tmp.Val;
                }
            }
            if (tmp.Val > 0)
            {
                if (resultTmp.Val == null)
                {
                    resultTmp.Val = tmp.Val;
                }
                else
                {
                    resultTmp.Next = new ListNode();
                    resultTmp.Val = tmp.Val;
                }

                resultTmp = resultTmp.Next;
            }

            return res;
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
                    num1.Next.Val--;
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


            while (data.Next?.Val != null)
            {
                resTemp.Val = data.Val;
                resTemp.Next = new ListNode();
                resTemp = resTemp.Next;
                data = data.Next;
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

        private static bool _validateContent(ListNode list)
        {
            var res = true;
            var tmpNode = list;
            if (list == null)
                return res;

            while (tmpNode.Next != null && tmpNode.Next.Val != null)
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

        private static long _getLength(ListNode list)
        {
            var listLength = 0;
            var tmpNode = list;
            if (list != null)
                listLength = 1;
            while (tmpNode.Next != null && tmpNode.Next.Val != null)
            {
                ++listLength;
                tmpNode = tmpNode.Next;
            }
            return listLength;

        }
        #endregion
        #endregion
    }

    public static class Extensions
    {
        public static ListNode DeepClone(this ListNode obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (ListNode)formatter.Deserialize(ms);
            }
        }
    }

}