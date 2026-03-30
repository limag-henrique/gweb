using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using MS.Internal.PresentationCore;

namespace MS.Internal
{
	// Token: 0x020006A2 RID: 1698
	internal sealed class AbbreviatedGeometryParser
	{
		// Token: 0x06004A57 RID: 19031 RVA: 0x001216B8 File Offset: 0x00120AB8
		private void ThrowBadToken()
		{
			throw new FormatException(SR.Get("Parser_UnexpectedToken", new object[]
			{
				this._pathString,
				this._curIndex - 1
			}));
		}

		// Token: 0x06004A58 RID: 19032 RVA: 0x001216F4 File Offset: 0x00120AF4
		private bool More()
		{
			return this._curIndex < this._pathLength;
		}

		// Token: 0x06004A59 RID: 19033 RVA: 0x00121710 File Offset: 0x00120B10
		private bool SkipWhiteSpace(bool allowComma)
		{
			bool result = false;
			while (this.More())
			{
				char c = this._pathString[this._curIndex];
				switch (c)
				{
				case '\t':
				case '\n':
				case '\r':
					break;
				case '\v':
				case '\f':
					goto IL_4F;
				default:
					if (c != ' ')
					{
						if (c != ',')
						{
							goto IL_4F;
						}
						if (allowComma)
						{
							result = true;
							allowComma = false;
						}
						else
						{
							this.ThrowBadToken();
						}
					}
					break;
				}
				IL_63:
				this._curIndex++;
				continue;
				IL_4F:
				if ((c > ' ' && c <= 'z') || !char.IsWhiteSpace(c))
				{
					return result;
				}
				goto IL_63;
			}
			return result;
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x00121798 File Offset: 0x00120B98
		private bool ReadToken()
		{
			this.SkipWhiteSpace(false);
			if (this.More())
			{
				string pathString = this._pathString;
				int curIndex = this._curIndex;
				this._curIndex = curIndex + 1;
				this._token = pathString[curIndex];
				return true;
			}
			return false;
		}

		// Token: 0x06004A5B RID: 19035 RVA: 0x001217DC File Offset: 0x00120BDC
		private bool IsNumber(bool allowComma)
		{
			bool flag = this.SkipWhiteSpace(allowComma);
			if (this.More())
			{
				this._token = this._pathString[this._curIndex];
				if (this._token == '.' || this._token == '-' || this._token == '+' || (this._token >= '0' && this._token <= '9') || this._token == 'I' || this._token == 'N')
				{
					return true;
				}
			}
			if (flag)
			{
				this.ThrowBadToken();
			}
			return false;
		}

		// Token: 0x06004A5C RID: 19036 RVA: 0x00121864 File Offset: 0x00120C64
		private void SkipDigits(bool signAllowed)
		{
			if (signAllowed && this.More() && (this._pathString[this._curIndex] == '-' || this._pathString[this._curIndex] == '+'))
			{
				this._curIndex++;
			}
			while (this.More() && this._pathString[this._curIndex] >= '0' && this._pathString[this._curIndex] <= '9')
			{
				this._curIndex++;
			}
		}

		// Token: 0x06004A5D RID: 19037 RVA: 0x001218F8 File Offset: 0x00120CF8
		private double ReadNumber(bool allowComma)
		{
			if (!this.IsNumber(allowComma))
			{
				this.ThrowBadToken();
			}
			bool flag = true;
			int i = this._curIndex;
			if (this.More() && (this._pathString[this._curIndex] == '-' || this._pathString[this._curIndex] == '+'))
			{
				this._curIndex++;
			}
			if (this.More() && this._pathString[this._curIndex] == 'I')
			{
				this._curIndex = Math.Min(this._curIndex + 8, this._pathLength);
				flag = false;
			}
			else if (this.More() && this._pathString[this._curIndex] == 'N')
			{
				this._curIndex = Math.Min(this._curIndex + 3, this._pathLength);
				flag = false;
			}
			else
			{
				this.SkipDigits(false);
				if (this.More() && this._pathString[this._curIndex] == '.')
				{
					flag = false;
					this._curIndex++;
					this.SkipDigits(false);
				}
				if (this.More() && (this._pathString[this._curIndex] == 'E' || this._pathString[this._curIndex] == 'e'))
				{
					flag = false;
					this._curIndex++;
					this.SkipDigits(true);
				}
			}
			if (flag && this._curIndex <= i + 8)
			{
				int num = 1;
				if (this._pathString[i] == '+')
				{
					i++;
				}
				else if (this._pathString[i] == '-')
				{
					i++;
					num = -1;
				}
				int num2 = 0;
				while (i < this._curIndex)
				{
					num2 = num2 * 10 + (int)(this._pathString[i] - '0');
					i++;
				}
				return (double)(num2 * num);
			}
			string value = this._pathString.Substring(i, this._curIndex - i);
			double result;
			try
			{
				result = Convert.ToDouble(value, this._formatProvider);
			}
			catch (FormatException innerException)
			{
				throw new FormatException(SR.Get("Parser_UnexpectedToken", new object[]
				{
					this._pathString,
					i
				}), innerException);
			}
			return result;
		}

		// Token: 0x06004A5E RID: 19038 RVA: 0x00121B38 File Offset: 0x00120F38
		private bool ReadBool()
		{
			this.SkipWhiteSpace(true);
			if (this.More())
			{
				string pathString = this._pathString;
				int curIndex = this._curIndex;
				this._curIndex = curIndex + 1;
				this._token = pathString[curIndex];
				if (this._token == '0')
				{
					return false;
				}
				if (this._token == '1')
				{
					return true;
				}
			}
			this.ThrowBadToken();
			return false;
		}

		// Token: 0x06004A5F RID: 19039 RVA: 0x00121B98 File Offset: 0x00120F98
		private Point ReadPoint(char cmd, bool allowcomma)
		{
			double num = this.ReadNumber(allowcomma);
			double num2 = this.ReadNumber(true);
			if (cmd >= 'a')
			{
				num += this._lastPoint.X;
				num2 += this._lastPoint.Y;
			}
			return new Point(num, num2);
		}

		// Token: 0x06004A60 RID: 19040 RVA: 0x00121BE0 File Offset: 0x00120FE0
		private Point Reflect()
		{
			return new Point(2.0 * this._lastPoint.X - this._secondLastPoint.X, 2.0 * this._lastPoint.Y - this._secondLastPoint.Y);
		}

		// Token: 0x06004A61 RID: 19041 RVA: 0x00121C34 File Offset: 0x00121034
		private void EnsureFigure()
		{
			if (!this._figureStarted)
			{
				this._context.BeginFigure(this._lastStart, true, false);
				this._figureStarted = true;
			}
		}

		// Token: 0x06004A62 RID: 19042 RVA: 0x00121C64 File Offset: 0x00121064
		internal void ParseToGeometryContext(StreamGeometryContext context, string pathString, int startIndex)
		{
			this._formatProvider = CultureInfo.InvariantCulture;
			this._context = context;
			this._pathString = pathString;
			this._pathLength = pathString.Length;
			this._curIndex = startIndex;
			this._secondLastPoint = new Point(0.0, 0.0);
			this._lastPoint = new Point(0.0, 0.0);
			this._lastStart = new Point(0.0, 0.0);
			this._figureStarted = false;
			bool flag = true;
			char c = ' ';
			while (this.ReadToken())
			{
				char token = this._token;
				if (flag)
				{
					if (token != 'M' && token != 'm')
					{
						this.ThrowBadToken();
					}
					flag = false;
				}
				if (token <= 'Z')
				{
					if (token <= 'C')
					{
						if (token == 'A')
						{
							goto IL_3B2;
						}
						if (token != 'C')
						{
							goto IL_447;
						}
						goto IL_2B2;
					}
					else
					{
						if (token == 'H')
						{
							goto IL_1DE;
						}
						switch (token)
						{
						case 'L':
						case 'V':
							goto IL_1DE;
						case 'M':
							break;
						case 'N':
						case 'O':
						case 'P':
						case 'R':
						case 'U':
							goto IL_447;
						case 'Q':
						case 'T':
							goto IL_332;
						case 'S':
							goto IL_2B2;
						default:
							if (token != 'Z')
							{
								goto IL_447;
							}
							goto IL_422;
						}
					}
				}
				else if (token <= 'c')
				{
					if (token == 'a')
					{
						goto IL_3B2;
					}
					if (token != 'c')
					{
						goto IL_447;
					}
					goto IL_2B2;
				}
				else
				{
					if (token == 'h')
					{
						goto IL_1DE;
					}
					switch (token)
					{
					case 'l':
					case 'v':
						goto IL_1DE;
					case 'm':
						break;
					case 'n':
					case 'o':
					case 'p':
					case 'r':
					case 'u':
						goto IL_447;
					case 'q':
					case 't':
						goto IL_332;
					case 's':
						goto IL_2B2;
					default:
						if (token != 'z')
						{
							goto IL_447;
						}
						goto IL_422;
					}
				}
				this._lastPoint = this.ReadPoint(token, false);
				context.BeginFigure(this._lastPoint, true, false);
				this._figureStarted = true;
				this._lastStart = this._lastPoint;
				c = 'M';
				while (this.IsNumber(true))
				{
					this._lastPoint = this.ReadPoint(token, false);
					context.LineTo(this._lastPoint, true, false);
					c = 'L';
				}
				continue;
				IL_1DE:
				this.EnsureFigure();
				do
				{
					if (token <= 'V')
					{
						if (token != 'H')
						{
							if (token != 'L')
							{
								if (token == 'V')
								{
									this._lastPoint.Y = this.ReadNumber(false);
								}
							}
							else
							{
								this._lastPoint = this.ReadPoint(token, false);
							}
						}
						else
						{
							this._lastPoint.X = this.ReadNumber(false);
						}
					}
					else if (token != 'h')
					{
						if (token != 'l')
						{
							if (token == 'v')
							{
								this._lastPoint.Y = this._lastPoint.Y + this.ReadNumber(false);
							}
						}
						else
						{
							this._lastPoint = this.ReadPoint(token, false);
						}
					}
					else
					{
						this._lastPoint.X = this._lastPoint.X + this.ReadNumber(false);
					}
					context.LineTo(this._lastPoint, true, false);
				}
				while (this.IsNumber(true));
				c = 'L';
				continue;
				IL_2B2:
				this.EnsureFigure();
				do
				{
					Point point;
					if (token == 's' || token == 'S')
					{
						if (c == 'C')
						{
							point = this.Reflect();
						}
						else
						{
							point = this._lastPoint;
						}
						this._secondLastPoint = this.ReadPoint(token, false);
					}
					else
					{
						point = this.ReadPoint(token, false);
						this._secondLastPoint = this.ReadPoint(token, true);
					}
					this._lastPoint = this.ReadPoint(token, true);
					context.BezierTo(point, this._secondLastPoint, this._lastPoint, true, false);
					c = 'C';
				}
				while (this.IsNumber(true));
				continue;
				IL_332:
				this.EnsureFigure();
				do
				{
					if (token == 't' || token == 'T')
					{
						if (c == 'Q')
						{
							this._secondLastPoint = this.Reflect();
						}
						else
						{
							this._secondLastPoint = this._lastPoint;
						}
						this._lastPoint = this.ReadPoint(token, false);
					}
					else
					{
						this._secondLastPoint = this.ReadPoint(token, false);
						this._lastPoint = this.ReadPoint(token, true);
					}
					context.QuadraticBezierTo(this._secondLastPoint, this._lastPoint, true, false);
					c = 'Q';
				}
				while (this.IsNumber(true));
				continue;
				IL_3B2:
				this.EnsureFigure();
				do
				{
					double width = this.ReadNumber(false);
					double height = this.ReadNumber(true);
					double rotationAngle = this.ReadNumber(true);
					bool isLargeArc = this.ReadBool();
					bool flag2 = this.ReadBool();
					this._lastPoint = this.ReadPoint(token, true);
					context.ArcTo(this._lastPoint, new Size(width, height), rotationAngle, isLargeArc, flag2 ? SweepDirection.Clockwise : SweepDirection.Counterclockwise, true, false);
				}
				while (this.IsNumber(true));
				c = 'A';
				continue;
				IL_422:
				this.EnsureFigure();
				context.SetClosedState(true);
				this._figureStarted = false;
				c = 'Z';
				this._lastPoint = this._lastStart;
				continue;
				IL_447:
				this.ThrowBadToken();
			}
		}

		// Token: 0x04001F6F RID: 8047
		private const bool AllowSign = true;

		// Token: 0x04001F70 RID: 8048
		private const bool AllowComma = true;

		// Token: 0x04001F71 RID: 8049
		private const bool IsFilled = true;

		// Token: 0x04001F72 RID: 8050
		private const bool IsClosed = true;

		// Token: 0x04001F73 RID: 8051
		private const bool IsStroked = true;

		// Token: 0x04001F74 RID: 8052
		private const bool IsSmoothJoin = true;

		// Token: 0x04001F75 RID: 8053
		private IFormatProvider _formatProvider;

		// Token: 0x04001F76 RID: 8054
		private string _pathString;

		// Token: 0x04001F77 RID: 8055
		private int _pathLength;

		// Token: 0x04001F78 RID: 8056
		private int _curIndex;

		// Token: 0x04001F79 RID: 8057
		private bool _figureStarted;

		// Token: 0x04001F7A RID: 8058
		private Point _lastStart;

		// Token: 0x04001F7B RID: 8059
		private Point _lastPoint;

		// Token: 0x04001F7C RID: 8060
		private Point _secondLastPoint;

		// Token: 0x04001F7D RID: 8061
		private char _token;

		// Token: 0x04001F7E RID: 8062
		private StreamGeometryContext _context;
	}
}
