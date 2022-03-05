using System;
using System.Collections.Generic;

namespace Core.Entities
{
	public class TypeCheckedString<Check1, Check2, Check3> : IEquatable<TypeCheckedString<Check1, Check2, Check3>>
		where Check1: StringCheck, new()
		where Check2: StringCheck, new()
		where Check3: StringCheck, new()
	{
		public TypeCheckedString(string value)
		{
			if(!(_check1.Check(value) && _check2.Check(value) && _check3.Check(value)))
			{
				throw new ArgumentException($"The String {value} does not pass checks");
			}
			_value = value;
		}

		public TypeCheckedString<Check1, Check2, Check3> ToCheckedString()
		{
			return this;
		}

		public string ToDefaultString()
		{
			return _value;
		}

        public bool Equals(TypeCheckedString<Check1, Check2, Check3> obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (_value is null && obj._value is null)
            {
                return true;
            }
            return _value?.Equals(obj?._value) ?? false;
        }

        private Check1 _check1 = new Check1();
		private Check2 _check2 = new Check2();
		private Check3 _check3 = new Check3();
		private string _value;
	}

	public interface CompileTimeInt
	{
		int Value();
	}

    public class CtInt1 : CompileTimeInt
    {
        public int Value()
        {
            return 1;
        }
    }
    public class CtInt10 : CompileTimeInt
    {
        public int Value()
        {
            return 10;
        }
    }

	public class CtInt15 : CompileTimeInt
    {
        public int Value()
        {
            return 15;
        }
    }

    public class CtInt20 : CompileTimeInt
	{
		public int Value()
		{
			return 20;
		}
	}

    public class CtInt25 : CompileTimeInt
    {
        public int Value()
        {
            return 25;
        }
    }

	public class CtInt30 : CompileTimeInt
	{
		public int Value()
		{
			return 30;
		}
	}

    public class CtInt40 : CompileTimeInt
    {
        public int Value()
        {
            return 40;
        }
    }

    public class CtInt50 : CompileTimeInt
    {
        public int Value()
        {
            return 50;
        }
    }

    public class CtInt80 : CompileTimeInt
    {
        public int Value()
        {
            return 80;
        }
    }

    public class CtInt200 : CompileTimeInt
    {
        public int Value()
        {
            return 200;
        }
    }

	public class CtInt250 : CompileTimeInt
    {
        public int Value()
        {
            return 250;
        }
    }

    public class CtInt4000: CompileTimeInt
	{
		public int Value()
		{
			return 4000;
		}
	}

	public interface StringCheck
	{
		bool Check(string value);
	}

	public class MaxLength<CtInt> : StringCheck where CtInt : CompileTimeInt, new()
	{
		private CtInt _maxLength = new CtInt();

		public bool Check(string value)
		{
            if(value == null)
            {
                return true;
            }

			return value.Length <= _maxLength.Value();
		}
	}

	public class NoCheck : StringCheck
	{
		public bool Check(string value)
		{
			return true;
		}
	}

	public interface CharBlacklist
	{
		List<int> GetBlacklist();
	}

	public class NewLines : CharBlacklist
	{
		// see Unicode Newline Guidelines at: http://www.unicode.org/reports/tr13/tr13-9.html
		private static readonly List<int> _newLineBlacklist = new List<int>
		{
			0x0d, // CR
			0x0a, // LF
			0x85, // NEL
			0x0b, // VT
			0x0c, // FF
			0x2028, // LS
			0x2029 // PS
		};

		public List<int> GetBlacklist()
		{
			return _newLineBlacklist;
		}
	}

	public class Blacklist<BlacklistedChars> : StringCheck
		where BlacklistedChars : CharBlacklist, new()
	{
		public bool Check(string value)
		{
            if(value == null)
            {
                return true;
            }

			var blacklist = _blacklist.GetBlacklist();
			for(int i = 0; i < value.Length; i += Char.IsSurrogatePair(value, i) ? 2 : 1)
			{
				if(blacklist.Contains(Char.ConvertToUtf32(value, i)))
				{
					return false;
				}
			}
			return true;
		}

		private BlacklistedChars _blacklist = new BlacklistedChars();
	}

    // TODO: test?
    public class NoWhiteSpace : StringCheck
    {
        public bool Check(string value)
        {
            for (int i = 0; i < value.Length; i += Char.IsSurrogatePair(value, i) ? 2 : 1)
            {
                if (Char.IsWhiteSpace(value, i))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
