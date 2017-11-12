//-----------------------------------------------------------------------
// <copyright file="CodeQr.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode
{
	using System;
	using System.Drawing;
	using System.IO;
	using System.Text;
	using Zen.Barcode.Properties;

	/// <summary>
	/// <c>CodeQrBarcodeDraw</c> extends <see cref="BarcodeDraw"/> to support
	/// rendering QR barcodes.
	/// </summary>
	public class CodeQrBarcodeDraw : BarcodeDraw
	{
		#region Internal Objects
		private class QRCodeEncoder
		{
			#region Internal Objects
			public class QRCodeUtility
			{
				public static bool IsUnicode(string value)
				{
					byte[] ascii = AsciiStringToByteArray(value);
					byte[] unicode = UnicodeStringToByteArray(value);
					string value1 = FromASCIIByteArray(ascii);
					string value2 = FromUnicodeByteArray(unicode);
					if (value1 != value2)
						return true;
					return false;
				}

				public static bool IsUnicode(byte[] byteData)
				{
					string value1 = FromASCIIByteArray(byteData);
					string value2 = FromUnicodeByteArray(byteData);
					byte[] ascii = AsciiStringToByteArray(value1);
					byte[] unicode = UnicodeStringToByteArray(value2);
					if (ascii[0] != unicode[0])
						return true;
					return false;
				}

				public static String FromASCIIByteArray(byte[] characters)
				{
					ASCIIEncoding encoding = new ASCIIEncoding();
					String constructedString = encoding.GetString(characters);
					return constructedString;
				}

				public static String FromUnicodeByteArray(byte[] characters)
				{
					UnicodeEncoding encoding = new UnicodeEncoding();
					String constructedString = encoding.GetString(characters);
					return constructedString;
				}

				public static byte[] AsciiStringToByteArray(String str)
				{
					ASCIIEncoding encoding = new ASCIIEncoding();
					return encoding.GetBytes(str);
				}

				public static byte[] UnicodeStringToByteArray(String str)
				{
					UnicodeEncoding encoding = new UnicodeEncoding();
					return encoding.GetBytes(str);
				}
			}

			public class SystemUtils
			{
				/// <summary>Reads a number of characters from the current source Stream and writes the data to the target array at the specified index.</summary>
				/// <param name="sourceStream">The source Stream to read from.</param>
				/// <param name="target">Contains the array of characteres read from the source Stream.</param>
				/// <param name="start">The starting index of the target array.</param>
				/// <param name="count">The maximum number of characters to read from the source Stream.</param>
				/// <returns>The number of characters read. The number will be less than or equal to count depending on the data available in the source Stream. Returns -1 if the end of the stream is reached.</returns>
				public static System.Int32 ReadInput(System.IO.Stream sourceStream, sbyte[] target, int start, int count)
				{
					// Returns 0 bytes if not enough space in target
					if (target.Length == 0)
						return 0;

					byte[] receiver = new byte[target.Length];
					int bytesRead = sourceStream.Read(receiver, start, count);

					// Returns -1 if EOF
					if (bytesRead == 0)
						return -1;

					for (int i = start; i < start + bytesRead; i++)
						target[i] = (sbyte)receiver[i];

					return bytesRead;
				}

				/// <summary>Reads a number of characters from the current source TextReader and writes the data to the target array at the specified index.</summary>
				/// <param name="sourceTextReader">The source TextReader to read from</param>
				/// <param name="target">Contains the array of characteres read from the source TextReader.</param>
				/// <param name="start">The starting index of the target array.</param>
				/// <param name="count">The maximum number of characters to read from the source TextReader.</param>
				/// <returns>The number of characters read. The number will be less than or equal to count depending on the data available in the source TextReader. Returns -1 if the end of the stream is reached.</returns>
				public static System.Int32 ReadInput(System.IO.TextReader sourceTextReader, short[] target, int start, int count)
				{
					// Returns 0 bytes if not enough space in target
					if (target.Length == 0)
						return 0;

					char[] charArray = new char[target.Length];
					int bytesRead = sourceTextReader.Read(charArray, start, count);

					// Returns -1 if EOF
					if (bytesRead == 0)
						return -1;

					for (int index = start; index < start + bytesRead; index++)
						target[index] = (short)charArray[index];

					return bytesRead;
				}

				/*******************************/
				/// <summary>
				/// Writes the exception stack trace to the received stream
				/// </summary>
				/// <param name="throwable">Exception to obtain information from</param>
				/// <param name="stream">Output sream used to write to</param>
				public static void WriteStackTrace(System.Exception throwable, System.IO.TextWriter stream)
				{
					stream.Write(throwable.StackTrace);
					stream.Flush();
				}

				/// <summary>
				/// Performs an unsigned bitwise right shift with the specified number
				/// </summary>
				/// <param name="number">Number to operate on</param>
				/// <param name="bits">Ammount of bits to shift</param>
				/// <returns>The resulting number from the shift operation</returns>
				public static int URShift(int number, int bits)
				{
					if (number >= 0)
						return number >> bits;
					else
						return (number >> bits) + (2 << ~bits);
				}

				/// <summary>
				/// Performs an unsigned bitwise right shift with the specified number
				/// </summary>
				/// <param name="number">Number to operate on</param>
				/// <param name="bits">Ammount of bits to shift</param>
				/// <returns>The resulting number from the shift operation</returns>
				public static int URShift(int number, long bits)
				{
					return URShift(number, (int)bits);
				}

				/// <summary>
				/// Performs an unsigned bitwise right shift with the specified number
				/// </summary>
				/// <param name="number">Number to operate on</param>
				/// <param name="bits">Ammount of bits to shift</param>
				/// <returns>The resulting number from the shift operation</returns>
				public static long URShift(long number, int bits)
				{
					if (number >= 0)
						return number >> bits;
					else
						return (number >> bits) + (2L << ~bits);
				}

				/// <summary>
				/// Performs an unsigned bitwise right shift with the specified number
				/// </summary>
				/// <param name="number">Number to operate on</param>
				/// <param name="bits">Ammount of bits to shift</param>
				/// <returns>The resulting number from the shift operation</returns>
				public static long URShift(long number, long bits)
				{
					return URShift(number, (int)bits);
				}

				/*******************************/
				/// <summary>
				/// Converts an array of sbytes to an array of bytes
				/// </summary>
				/// <param name="sbyteArray">The array of sbytes to be converted</param>
				/// <returns>The new array of bytes</returns>
				public static byte[] ToByteArray(sbyte[] sbyteArray)
				{
					byte[] byteArray = null;

					if (sbyteArray != null)
					{
						byteArray = new byte[sbyteArray.Length];
						for (int index = 0; index < sbyteArray.Length; index++)
							byteArray[index] = (byte)sbyteArray[index];
					}
					return byteArray;
				}

				/// <summary>
				/// Converts a string to an array of bytes
				/// </summary>
				/// <param name="sourceString">The string to be converted</param>
				/// <returns>The new array of bytes</returns>
				public static byte[] ToByteArray(String sourceString)
				{
					return System.Text.UTF8Encoding.UTF8.GetBytes(sourceString);
				}

				/// <summary>
				/// Converts a array of object-type instances to a byte-type array.
				/// </summary>
				/// <param name="tempObjectArray">Array to convert.</param>
				/// <returns>An array of byte type elements.</returns>
				public static byte[] ToByteArray(System.Object[] tempObjectArray)
				{
					byte[] byteArray = null;
					if (tempObjectArray != null)
					{
						byteArray = new byte[tempObjectArray.Length];
						for (int index = 0; index < tempObjectArray.Length; index++)
							byteArray[index] = (byte)tempObjectArray[index];
					}
					return byteArray;
				}

				/*******************************/
				/// <summary>
				/// Receives a byte array and returns it transformed in an sbyte array
				/// </summary>
				/// <param name="byteArray">Byte array to process</param>
				/// <returns>The transformed array</returns>
				public static sbyte[] ToSByteArray(byte[] byteArray)
				{
					sbyte[] sbyteArray = null;
					if (byteArray != null)
					{
						sbyteArray = new sbyte[byteArray.Length];
						for (int index = 0; index < byteArray.Length; index++)
							sbyteArray[index] = (sbyte)byteArray[index];
					}
					return sbyteArray;
				}


				/*******************************/
				/// <summary>
				/// Converts an array of sbytes to an array of chars
				/// </summary>
				/// <param name="sByteArray">The array of sbytes to convert</param>
				/// <returns>The new array of chars</returns>
				public static char[] ToCharArray(sbyte[] sByteArray)
				{
					return System.Text.UTF8Encoding.UTF8.GetChars(ToByteArray(sByteArray));
				}

				/// <summary>
				/// Converts an array of bytes to an array of chars
				/// </summary>
				/// <param name="byteArray">The array of bytes to convert</param>
				/// <returns>The new array of chars</returns>
				public static char[] ToCharArray(byte[] byteArray)
				{
					return System.Text.UTF8Encoding.UTF8.GetChars(byteArray);
				}

			}
			#endregion

			#region Private Fields
			private QrErrorCorrection _errorCorrect;
			private QrEncodeMode _encodeMode;
			private int _version;

			private int _structureAppendN;
			private int _structureAppendM;
			private int _structureAppendParity;
			//private string _structureAppendOriginaldata;

			private int _scale;
			private Color _backgroundColor;
			private Color _foregroundColor;
			#endregion

			#region Public Constructors
			/// <summary>
			/// Constructor
			/// </summary>
			public QRCodeEncoder()
			{
				_errorCorrect = QrErrorCorrection.M;
				_encodeMode = QrEncodeMode.Byte;
				_version = 7;

				_structureAppendN = 0;
				_structureAppendM = 0;
				_structureAppendParity = 0;
				//_structureAppendOriginaldata = "";

				_scale = 4;
				_backgroundColor = Color.White;
				_foregroundColor = Color.Black;

				//QRCODE_DATA_PATH = Environment.CurrentDirectory + @"\" + DATA_PATH;
			}
			#endregion

			#region Public Properties
			public QrErrorCorrection ErrorCorrect
			{
				get
				{
					return _errorCorrect;
				}
				set
				{
					_errorCorrect = value;
				}
			}

			public int Version
			{
				get
				{
					return _version;
				}
				set
				{
					if (value >= 0 && value <= 40)
					{
						_version = value;
					}
				}
			}

			public QrEncodeMode EncodeMode
			{
				get
				{
					return _encodeMode;
				}
				set
				{
					_encodeMode = value;
				}
			}

			public int Scale
			{
				get
				{
					return _scale;
				}
				set
				{
					_scale = value;
				}
			}

			public Color BackgroundColor
			{
				get
				{
					return _backgroundColor;
				}
				set
				{
					_backgroundColor = value;
				}
			}

			public Color ForegroundColor
			{
				get
				{
					return _foregroundColor;
				}
				set
				{
					_foregroundColor = value;
				}
			}
			#endregion

			/// <summary>
			/// Sets the structure append parameters
			/// </summary>
			/// <param name="m">The m value (between 2 and 16 inclusive).</param>
			/// <param name="n">The n value (between 1 and 16 inclusive).</param>
			/// <param name="p">The p value (between 0 and 255 inclusive).</param>
			public virtual void SetStructureAppend(int m, int n, int p)
			{
				if (n > 1 && n <= 16 && m > 0 && m <= 16 && p >= 0 && p <= 255)
				{
					_structureAppendM = m;
					_structureAppendN = n;
					_structureAppendParity = p;
				}
			}

			public virtual int CalculateStructureAppendParity(sbyte[] originaldata)
			{
				int originaldataLength;
				int i = 0;
				int structureAppendParity = 0;

				originaldataLength = originaldata.Length;

				if (originaldataLength > 1)
				{
					structureAppendParity = 0;
					while (i < originaldataLength)
					{
						structureAppendParity = (structureAppendParity ^ (originaldata[i] & 0xFF));
						i++;
					}
				}
				else
				{
					structureAppendParity = -1;
				}
				return structureAppendParity;
			}

			public virtual bool[][] CalculateQrCode(byte[] qrcodeData)
			{
				int dataLength;
				int dataCounter = 0;

				dataLength = qrcodeData.Length;

				int[] dataValue = new int[dataLength + 32];
				sbyte[] dataBits = new sbyte[dataLength + 32];

				if (dataLength <= 0)
				{
					bool[][] ret = new bool[][] { new bool[] { false } };
					return ret;
				}

				if (_structureAppendN > 1)
				{
					dataValue[0] = 3;
					dataBits[0] = 4;

					dataValue[1] = _structureAppendM - 1;
					dataBits[1] = 4;

					dataValue[2] = _structureAppendN - 1;
					dataBits[2] = 4;

					dataValue[3] = _structureAppendParity;
					dataBits[3] = 8;

					dataCounter = 4;
				}
				dataBits[dataCounter] = 4;

				/*  --- determine encode mode --- */

				int[] codewordNumPlus;
				int codewordNumCounterValue;

				switch (_encodeMode)
				{
					/* ---- alphanumeric mode ---  */
					case QrEncodeMode.AlphaNumeric:
						codewordNumPlus = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
						dataValue[dataCounter] = 2;
						dataCounter++;
						dataValue[dataCounter] = dataLength;
						dataBits[dataCounter] = 9;
						codewordNumCounterValue = dataCounter;

						dataCounter++;
						for (int i = 0; i < dataLength; i++)
						{
							char chr = (char)qrcodeData[i];
							sbyte chrValue = 0;
							if (chr >= 48 && chr < 58)
							{
								chrValue = (sbyte)(chr - 48);
							}
							else
							{
								if (chr >= 65 && chr < 91)
								{
									chrValue = (sbyte)(chr - 55);
								}
								else
								{
									if (chr == 32)
									{
										chrValue = 36;
									}
									if (chr == 36)
									{
										chrValue = 37;
									}
									if (chr == 37)
									{
										chrValue = 38;
									}
									if (chr == 42)
									{
										chrValue = 39;
									}
									if (chr == 43)
									{
										chrValue = 40;
									}
									if (chr == 45)
									{
										chrValue = 41;
									}
									if (chr == 46)
									{
										chrValue = 42;
									}
									if (chr == 47)
									{
										chrValue = 43;
									}
									if (chr == 58)
									{
										chrValue = 44;
									}
								}
							}
							if ((i % 2) == 0)
							{
								dataValue[dataCounter] = chrValue;
								dataBits[dataCounter] = 6;
							}
							else
							{
								dataValue[dataCounter] = dataValue[dataCounter] * 45 + chrValue;
								dataBits[dataCounter] = 11;
								if (i < dataLength - 1)
								{
									dataCounter++;
								}
							}
						}
						dataCounter++;
						break;

					/* ---- numeric mode ---- */

					case QrEncodeMode.Numeric:
						codewordNumPlus = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
						dataValue[dataCounter] = 1;
						dataCounter++;
						dataValue[dataCounter] = dataLength;
						dataBits[dataCounter] = 10; /* #version 1-9*/
						codewordNumCounterValue = dataCounter;

						dataCounter++;
						for (int i = 0; i < dataLength; i++)
						{
							if ((i % 3) == 0)
							{
								dataValue[dataCounter] = (int)(qrcodeData[i] - 0x30);
								dataBits[dataCounter] = 4;
							}
							else
							{
								dataValue[dataCounter] = dataValue[dataCounter] * 10 + (int)(qrcodeData[i] - 0x30);

								if ((i % 3) == 1)
								{
									dataBits[dataCounter] = 7;
								}
								else
								{
									dataBits[dataCounter] = 10;
									if (i < dataLength - 1)
									{
										dataCounter++;
									}
								}
							}
						}
						dataCounter++;
						break;

					/* ---- 8bit byte ---- */
					default:
						codewordNumPlus = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8 };
						dataValue[dataCounter] = 4;
						dataCounter++;
						dataValue[dataCounter] = dataLength;
						dataBits[dataCounter] = 8; /* #version 1-9 */
						codewordNumCounterValue = dataCounter;

						dataCounter++;
						for (int i = 0; i < dataLength; i++)
						{
							dataValue[i + dataCounter] = (qrcodeData[i] & 0xFF);
							dataBits[i + dataCounter] = 8;
						}
						dataCounter += dataLength;
						break;
				}

				int totalDataBits = 0;
				for (int i = 0; i < dataCounter; i++)
				{
					totalDataBits += dataBits[i];
				}

				sbyte ec = (sbyte)_errorCorrect;
				int[][] maxDataBitsArray = new int[][] { new int[] { 0, 128, 224, 352, 512, 688, 864, 992, 1232, 1456, 1728, 2032, 2320, 2672, 2920, 3320, 3624, 4056, 4504, 5016, 5352, 5712, 6256, 6880, 7312, 8000, 8496, 9024, 9544, 10136, 10984, 11640, 12328, 13048, 13800, 14496, 15312, 15936, 16816, 17728, 18672 }, new int[] { 0, 152, 272, 440, 640, 864, 1088, 1248, 1552, 1856, 2192, 2592, 2960, 3424, 3688, 4184, 4712, 5176, 5768, 6360, 6888, 7456, 8048, 8752, 9392, 10208, 10960, 11744, 12248, 13048, 13880, 14744, 15640, 16568, 17528, 18448, 19472, 20528, 21616, 22496, 23648 }, new int[] { 0, 72, 128, 208, 288, 368, 480, 528, 688, 800, 976, 1120, 1264, 1440, 1576, 1784, 2024, 2264, 2504, 2728, 3080, 3248, 3536, 3712, 4112, 4304, 4768, 5024, 5288, 5608, 5960, 6344, 6760, 7208, 7688, 7888, 8432, 8768, 9136, 9776, 10208 }, new int[] { 0, 104, 176, 272, 384, 496, 608, 704, 880, 1056, 1232, 1440, 1648, 1952, 2088, 2360, 2600, 2936, 3176, 3560, 3880, 4096, 4544, 4912, 5312, 5744, 6032, 6464, 6968, 7288, 7880, 8264, 8920, 9368, 9848, 10288, 10832, 11408, 12016, 12656, 13328 } };
				int maxDataBits = 0;

				if (_version == 0)
				{
					/* auto version select */
					_version = 1;
					for (int i = 1; i <= 40; i++)
					{
						if ((maxDataBitsArray[ec][i]) >= totalDataBits + codewordNumPlus[_version])
						{
							maxDataBits = maxDataBitsArray[ec][i];
							break;
						}
						_version++;
					}
				}
				else
				{
					maxDataBits = maxDataBitsArray[ec][_version];
				}
				totalDataBits += codewordNumPlus[_version];
				dataBits[codewordNumCounterValue] = (sbyte)(dataBits[codewordNumCounterValue] + codewordNumPlus[_version]);

				int[] maxCodewordsArray = new int[] { 0, 26, 44, 70, 100, 134, 172, 196, 242, 292, 346, 404, 466, 532, 581, 655, 733, 815, 901, 991, 1085, 1156, 1258, 1364, 1474, 1588, 1706, 1828, 1921, 2051, 2185, 2323, 2465, 2611, 2761, 2876, 3034, 3196, 3362, 3532, 3706 };
				int maxCodewords = maxCodewordsArray[_version];
				int maxModules1side = 17 + (_version << 2);
				int[] matrixRemainBit = new int[] { 0, 0, 7, 7, 7, 7, 7, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0 };

				/* ---- read version ECC data file */
				int byte_num = matrixRemainBit[_version] + (maxCodewords << 3);
				sbyte[] matrixX = new sbyte[byte_num];
				sbyte[] matrixY = new sbyte[byte_num];
				sbyte[] maskArray = new sbyte[byte_num];
				sbyte[] formatInformationX2 = new sbyte[15];
				sbyte[] formatInformationY2 = new sbyte[15];
				sbyte[] rsEccCodewords = new sbyte[1];
				sbyte[] rsBlockOrderTemp = new sbyte[128];
				try
				{
					String fileName = "qrv" + Convert.ToString(_version) + "_" + Convert.ToString(ec);
					using (Stream memoryStream = new MemoryStream((byte[])Resources.ResourceManager.GetObject(fileName), false))
					{
						using (BufferedStream bis = new BufferedStream(memoryStream))
						{
							SystemUtils.ReadInput(bis, matrixX, 0, matrixX.Length);
							SystemUtils.ReadInput(bis, matrixY, 0, matrixY.Length);
							SystemUtils.ReadInput(bis, maskArray, 0, maskArray.Length);
							SystemUtils.ReadInput(bis, formatInformationX2, 0, formatInformationX2.Length);
							SystemUtils.ReadInput(bis, formatInformationY2, 0, formatInformationY2.Length);
							SystemUtils.ReadInput(bis, rsEccCodewords, 0, rsEccCodewords.Length);
							SystemUtils.ReadInput(bis, rsBlockOrderTemp, 0, rsBlockOrderTemp.Length);
						}
					}
				}
				catch (Exception e)
				{
					SystemUtils.WriteStackTrace(e, Console.Error);
				}

				sbyte rsBlockOrderLength = 1;
				for (byte i = 1; i < 128; i++)
				{
					if (rsBlockOrderTemp[i] == 0)
					{
						rsBlockOrderLength = (sbyte)i;
						break;
					}
				}
				sbyte[] rsBlockOrder = new sbyte[rsBlockOrderLength];
				Array.Copy(rsBlockOrderTemp, 0, rsBlockOrder, 0, (byte)rsBlockOrderLength);


				sbyte[] formatInformationX1 = new sbyte[] { 0, 1, 2, 3, 4, 5, 7, 8, 8, 8, 8, 8, 8, 8, 8 };
				sbyte[] formatInformationY1 = new sbyte[] { 8, 8, 8, 8, 8, 8, 8, 8, 7, 5, 4, 3, 2, 1, 0 };

				int maxDataCodewords = maxDataBits >> 3;

				/* -- read frame data  -- */

				int modules1Side = 4 * _version + 17;
				int matrixTotalBits = modules1Side * modules1Side;
				sbyte[] frameData = new sbyte[matrixTotalBits + modules1Side];

				try
				{
					//String filename = QRCODE_DATA_PATH + "/qrvfr" + System.Convert.ToString(qrcodeVersion) + ".dat";
					//StreamReader reader = new StreamReader(filename);

					String fileName = "qrvfr" + Convert.ToString(_version);
					Stream memoryStream = new MemoryStream((byte[])Resources.ResourceManager.GetObject(fileName), false);

					BufferedStream bis = new BufferedStream(memoryStream);
					SystemUtils.ReadInput(bis, frameData, 0, frameData.Length);
					bis.Close();
					memoryStream.Close();
					//reader.Close();
					//fis.Close();
				}
				catch (Exception e)
				{
					SystemUtils.WriteStackTrace(e, Console.Error);
				}

				/*  --- set terminator */
				if (totalDataBits <= maxDataBits - 4)
				{
					dataValue[dataCounter] = 0;
					dataBits[dataCounter] = 4;
				}
				else
				{
					if (totalDataBits < maxDataBits)
					{
						dataValue[dataCounter] = 0;
						dataBits[dataCounter] = (sbyte)(maxDataBits - totalDataBits);
					}
					else
					{
						if (totalDataBits > maxDataBits)
						{
							System.Console.Out.WriteLine("overflow");
						}
					}
				}
				sbyte[] dataCodewords = divideDataBy8Bits(dataValue, dataBits, maxDataCodewords);
				sbyte[] codewords = calculateRSECC(dataCodewords, rsEccCodewords[0], rsBlockOrder, maxDataCodewords, maxCodewords);

				/* ---- flash matrix */
				sbyte[][] matrixContent = new sbyte[modules1Side][];
				for (int i2 = 0; i2 < modules1Side; i2++)
				{
					matrixContent[i2] = new sbyte[modules1Side];
				}

				for (int i = 0; i < modules1Side; i++)
				{
					for (int j = 0; j < modules1Side; j++)
					{
						matrixContent[j][i] = 0;
					}
				}

				/* --- attach data */
				for (int i = 0; i < maxCodewords; i++)
				{
					sbyte codeword_i = codewords[i];
					for (int j = 7; j >= 0; j--)
					{
						int codewordBitsNumber = (i * 8) + j;
						matrixContent[matrixX[codewordBitsNumber] & 0xFF][matrixY[codewordBitsNumber] & 0xFF] = (sbyte)((255 * (codeword_i & 1)) ^ maskArray[codewordBitsNumber]);
						codeword_i = (sbyte)(SystemUtils.URShift((codeword_i & 0xFF), 1));
					}
				}

				for (int matrixRemain = matrixRemainBit[_version]; matrixRemain > 0; matrixRemain--)
				{
					int remainBitTemp = matrixRemain + (maxCodewords * 8) - 1;
					matrixContent[matrixX[remainBitTemp] & 0xFF][matrixY[remainBitTemp] & 0xFF] = (sbyte)(255 ^ maskArray[remainBitTemp]);
				}

				/* --- mask select --- */
				sbyte maskNumber = selectMask(matrixContent, matrixRemainBit[_version] + maxCodewords * 8);
				sbyte maskContent = (sbyte)(1 << maskNumber);

				/* --- format information --- */
				sbyte formatInformationValue = (sbyte)(((sbyte)(ec << 3)) | maskNumber);
				String[] formatInformationArray = new String[] { "101010000010010", "101000100100101", "101111001111100", "101101101001011", "100010111111001", "100000011001110", "100111110010111", "100101010100000", "111011111000100", "111001011110011", "111110110101010", "111100010011101", "110011000101111", "110001100011000", "110110001000001", "110100101110110", "001011010001001", "001001110111110", "001110011100111", "001100111010000", "000011101100010", "000001001010101", "000110100001100", "000100000111011", "011010101011111", "011000001101000", "011111100110001", "011101000000110", "010010010110100", "010000110000011", "010111011011010", "010101111101101" };
				for (int i = 0; i < 15; i++)
				{
					sbyte content = (sbyte)System.SByte.Parse(formatInformationArray[formatInformationValue].Substring(i, (i + 1) - (i)));
					matrixContent[formatInformationX1[i] & 0xFF][formatInformationY1[i] & 0xFF] = (sbyte)(content * 255);
					matrixContent[formatInformationX2[i] & 0xFF][formatInformationY2[i] & 0xFF] = (sbyte)(content * 255);
				}

				bool[][] out_Renamed = new bool[modules1Side][];
				for (int i3 = 0; i3 < modules1Side; i3++)
				{
					out_Renamed[i3] = new bool[modules1Side];
				}

				int c = 0;
				for (int i = 0; i < modules1Side; i++)
				{
					for (int j = 0; j < modules1Side; j++)
					{
						if ((matrixContent[j][i] & maskContent) != 0 || frameData[c] == (char)49)
						{
							out_Renamed[j][i] = true;
						}
						else
						{
							out_Renamed[j][i] = false;
						}
						c++;
					}
					c++;
				}

				return out_Renamed;
			}

			private static sbyte[] divideDataBy8Bits(int[] data, sbyte[] bits, int maxDataCodewords)
			{
				/* divide Data By 8bit and add padding char */
				int l1 = bits.Length;
				int l2;
				int codewordsCounter = 0;
				int remainingBits = 8;
				int max = 0;
				int buffer;
				int bufferBits;
				bool flag;

				if (l1 != data.Length)
				{
				}
				for (int i = 0; i < l1; i++)
				{
					max += bits[i];
				}
				l2 = (max - 1) / 8 + 1;
				sbyte[] codewords = new sbyte[maxDataCodewords];
				for (int i = 0; i < l2; i++)
				{
					codewords[i] = 0;
				}
				for (int i = 0; i < l1; i++)
				{
					buffer = data[i];
					bufferBits = bits[i];
					flag = true;

					if (bufferBits == 0)
					{
						break;
					}
					while (flag)
					{
						if (remainingBits > bufferBits)
						{
							codewords[codewordsCounter] = (sbyte)((codewords[codewordsCounter] << bufferBits) | buffer);
							remainingBits -= bufferBits;
							flag = false;
						}
						else
						{
							bufferBits -= remainingBits;
							codewords[codewordsCounter] = (sbyte)((codewords[codewordsCounter] << remainingBits) | (buffer >> bufferBits));

							if (bufferBits == 0)
							{
								flag = false;
							}
							else
							{
								buffer = (buffer & ((1 << bufferBits) - 1));
								flag = true;
							}
							codewordsCounter++;
							remainingBits = 8;
						}
					}
				}
				if (remainingBits != 8)
				{
					codewords[codewordsCounter] = (sbyte)(codewords[codewordsCounter] << remainingBits);
				}
				else
				{
					codewordsCounter--;
				}
				if (codewordsCounter < maxDataCodewords - 1)
				{
					flag = true;
					while (codewordsCounter < maxDataCodewords - 1)
					{
						codewordsCounter++;
						if (flag)
						{
							codewords[codewordsCounter] = -20;
						}
						else
						{
							codewords[codewordsCounter] = 17;
						}
						flag = !(flag);
					}
				}
				return codewords;
			}

			private static sbyte[] calculateRSECC(sbyte[] codewords, sbyte rsEccCodewords, sbyte[] rsBlockOrder, int maxDataCodewords, int maxCodewords)
			{
				sbyte[][] rsCalTableArray = new sbyte[256][];
				for (int i = 0; i < 256; i++)
				{
					rsCalTableArray[i] = new sbyte[rsEccCodewords];
				}
				try
				{
					String fileName = "rsc" + rsEccCodewords.ToString();
					using (Stream memoryStream = new MemoryStream((byte[])Resources.ResourceManager.GetObject(fileName), false))
					{
						using (BufferedStream bis = new BufferedStream(memoryStream))
						{
							for (int i = 0; i < 256; i++)
							{
								SystemUtils.ReadInput(bis, rsCalTableArray[i], 0, rsCalTableArray[i].Length);
							}
						}
					}
				}
				catch (Exception e)
				{
					SystemUtils.WriteStackTrace(e, Console.Error);
				}

				/* ---- RS-ECC prepare */
				int i2 = 0;
				int j = 0;
				int rsBlockNumber = 0;

				sbyte[][] rsTemp = new sbyte[rsBlockOrder.Length][];
				sbyte[] res = new sbyte[maxCodewords];
				Array.Copy(codewords, 0, res, 0, codewords.Length);

				i2 = 0;
				while (i2 < rsBlockOrder.Length)
				{
					rsTemp[i2] = new sbyte[(rsBlockOrder[i2] & 0xFF) - rsEccCodewords];
					i2++;
				}
				i2 = 0;
				while (i2 < maxDataCodewords)
				{
					rsTemp[rsBlockNumber][j] = codewords[i2];
					j++;
					if (j >= (rsBlockOrder[rsBlockNumber] & 0xFF) - rsEccCodewords)
					{
						j = 0;
						rsBlockNumber++;
					}
					i2++;
				}

				/* ---  RS-ECC main --- */
				rsBlockNumber = 0;
				while (rsBlockNumber < rsBlockOrder.Length)
				{
					sbyte[] rsTempData;
					rsTempData = new sbyte[rsTemp[rsBlockNumber].Length];
					rsTemp[rsBlockNumber].CopyTo(rsTempData, 0);

					int rsCodewords = (rsBlockOrder[rsBlockNumber] & 0xFF);
					int rsDataCodewords = rsCodewords - rsEccCodewords;

					j = rsDataCodewords;
					while (j > 0)
					{
						sbyte first = rsTempData[0];
						if (first != 0)
						{
							sbyte[] leftChr = new sbyte[rsTempData.Length - 1];
							Array.Copy(rsTempData, 1, leftChr, 0, rsTempData.Length - 1);
							sbyte[] cal = rsCalTableArray[(first & 0xFF)];
							rsTempData = calculateByteArrayBits(leftChr, cal, "xor");
						}
						else
						{
							if (rsEccCodewords < rsTempData.Length)
							{
								sbyte[] rsTempNew = new sbyte[rsTempData.Length - 1];
								Array.Copy(rsTempData, 1, rsTempNew, 0, rsTempData.Length - 1);
								rsTempData = new sbyte[rsTempNew.Length];
								rsTempNew.CopyTo(rsTempData, 0);
							}
							else
							{
								sbyte[] rsTempNew = new sbyte[rsEccCodewords];
								Array.Copy(rsTempData, 1, rsTempNew, 0, rsTempData.Length - 1);
								rsTempNew[rsEccCodewords - 1] = 0;
								rsTempData = new sbyte[rsTempNew.Length];
								rsTempNew.CopyTo(rsTempData, 0);
							}
						}
						j--;
					}

					Array.Copy(rsTempData, 0, res, codewords.Length + rsBlockNumber * rsEccCodewords, (byte)rsEccCodewords);
					rsBlockNumber++;
				}
				return res;
			}

			private static sbyte[] calculateByteArrayBits(sbyte[] xa, sbyte[] xb, String ind)
			{
				int ll;
				int ls;
				sbyte[] res;
				sbyte[] xl;
				sbyte[] xs;

				if (xa.Length > xb.Length)
				{
					xl = new sbyte[xa.Length];
					xa.CopyTo(xl, 0);
					xs = new sbyte[xb.Length];
					xb.CopyTo(xs, 0);
				}
				else
				{
					xl = new sbyte[xb.Length];
					xb.CopyTo(xl, 0);
					xs = new sbyte[xa.Length];
					xa.CopyTo(xs, 0);
				}
				ll = xl.Length;
				ls = xs.Length;
				res = new sbyte[ll];

				for (int i = 0; i < ll; i++)
				{
					if (i < ls)
					{
						if ((System.Object)ind == (System.Object)"xor")
						{
							res[i] = (sbyte)(xl[i] ^ xs[i]);
						}
						else
						{
							res[i] = (sbyte)(xl[i] | xs[i]);
						}
					}
					else
					{
						res[i] = xl[i];
					}
				}
				return res;
			}

			private static sbyte selectMask(sbyte[][] matrixContent, int maxCodewordsBitWithRemain)
			{
				int l = matrixContent.Length;
				int[] d1 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
				int[] d2 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
				int[] d3 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
				int[] d4 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };

				int d2And = 0;
				int d2Or = 0;
				int[] d4Counter = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };

				for (int y = 0; y < l; y++)
				{
					int[] xData = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
					int[] yData = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
					bool[] xD1Flag = new bool[] { false, false, false, false, false, false, false, false };
					bool[] yD1Flag = new bool[] { false, false, false, false, false, false, false, false };

					for (int x = 0; x < l; x++)
					{

						if (x > 0 && y > 0)
						{
							d2And = matrixContent[x][y] & matrixContent[x - 1][y] & matrixContent[x][y - 1] & matrixContent[x - 1][y - 1] & 0xFF;
							d2Or = (matrixContent[x][y] & 0xFF) | (matrixContent[x - 1][y] & 0xFF) | (matrixContent[x][y - 1] & 0xFF) | (matrixContent[x - 1][y - 1] & 0xFF);
						}

						for (int maskNumber = 0; maskNumber < 8; maskNumber++)
						{
							xData[maskNumber] = ((xData[maskNumber] & 63) << 1) | ((SystemUtils.URShift((matrixContent[x][y] & 0xFF), maskNumber)) & 1);
							yData[maskNumber] = ((yData[maskNumber] & 63) << 1) | ((SystemUtils.URShift((matrixContent[y][x] & 0xFF), maskNumber)) & 1);

							if ((matrixContent[x][y] & (1 << maskNumber)) != 0)
							{
								d4Counter[maskNumber]++;
							}

							if (xData[maskNumber] == 93)
							{
								d3[maskNumber] += 40;
							}

							if (yData[maskNumber] == 93)
							{
								d3[maskNumber] += 40;
							}

							if (x > 0 && y > 0)
							{
								if (((d2And & 1) != 0) || ((d2Or & 1) == 0))
								{
									d2[maskNumber] += 3;
								}

								d2And = d2And >> 1;
								d2Or = d2Or >> 1;
							}

							if (((xData[maskNumber] & 0x1F) == 0) || ((xData[maskNumber] & 0x1F) == 0x1F))
							{
								if (x > 3)
								{
									if (xD1Flag[maskNumber])
									{
										d1[maskNumber]++;
									}
									else
									{
										d1[maskNumber] += 3;
										xD1Flag[maskNumber] = true;
									}
								}
							}
							else
							{
								xD1Flag[maskNumber] = false;
							}
							if (((yData[maskNumber] & 0x1F) == 0) || ((yData[maskNumber] & 0x1F) == 0x1F))
							{
								if (x > 3)
								{
									if (yD1Flag[maskNumber])
									{
										d1[maskNumber]++;
									}
									else
									{
										d1[maskNumber] += 3;
										yD1Flag[maskNumber] = true;
									}
								}
							}
							else
							{
								yD1Flag[maskNumber] = false;
							}
						}
					}
				}

				int minValue = 0;
				sbyte res = 0;
				int[] d4Value = new int[] { 90, 80, 70, 60, 50, 40, 30, 20, 10, 0, 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 90 };
				for (int maskNumber = 0; maskNumber < 8; maskNumber++)
				{
					d4[maskNumber] = d4Value[(int)((20 * d4Counter[maskNumber]) / maxCodewordsBitWithRemain)];
					int demerit = d1[maskNumber] + d2[maskNumber] + d3[maskNumber] + d4[maskNumber];
					if (demerit < minValue || maskNumber == 0)
					{
						res = (sbyte)maskNumber;
						minValue = demerit;
					}
				}
				return res;
			}


			/// <summary>
			/// Encode the content using the encoding scheme given
			/// </summary>
			/// <param name="content">
			/// The content to render.
			/// </param>
			/// <param name="encoding">
			/// The character encoding of the content.
			/// </param>
			/// <returns>
			/// A <see cref="Bitmap"/> object representing the QR barcode.
			/// </returns>
			public virtual Bitmap Encode(String content, Encoding encoding)
			{
				bool[][] matrix = CalculateQrCode(encoding.GetBytes(content));
				SolidBrush brush = new SolidBrush(_backgroundColor);
				Bitmap image = new Bitmap((matrix.Length * _scale) + 1, (matrix.Length * _scale) + 1);
				Graphics g = Graphics.FromImage(image);
				g.FillRectangle(brush, new Rectangle(0, 0, image.Width, image.Height));
				brush.Color = _foregroundColor;
				for (int i = 0; i < matrix.Length; i++)
				{
					for (int j = 0; j < matrix.Length; j++)
					{
						if (matrix[j][i])
						{
							g.FillRectangle(brush, j * _scale, i * _scale, _scale, _scale);
						}
					}
				}
				return image;
			}

			/// <summary>
			/// Encode the content using an encoding scheme determined by the text.
			/// </summary>
			/// <param name="content">
			/// The content to render.
			/// </param>
			/// <returns>
			/// A <see cref="Bitmap"/> object representing the QR barcode.
			/// </returns>
			public virtual Bitmap Encode(String content)
			{
				if (QRCodeUtility.IsUnicode(content))
				{
					return Encode(content, Encoding.Unicode);
				}
				else
				{
					return Encode(content, Encoding.ASCII);
				}
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Draws the specified text using the supplied barcode metrics.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="metrics">A <see cref="T:Zen.Barcode.BarcodeMetrics"/> object.</param>
		/// <returns></returns>
		public override sealed Image Draw(string text, BarcodeMetrics metrics)
		{
			return DrawQr(text, (BarcodeMetricsQr)metrics);
		}

		/// <summary>
		/// Gets a <see cref="T:Zen.Barcode.BarcodeMetrics"/> object containing default
		/// settings for the specified maximum bar height.
		/// </summary>
		/// <param name="maxHeight">The maximum barcode height.</param>
		/// <returns>
		/// A <see cref="T:Zen.Barcode.BarcodeMetrics"/> object.
		/// </returns>
		public override BarcodeMetrics GetDefaultMetrics(int maxHeight)
		{
			QRCodeEncoder temp = new QRCodeEncoder();
			return new BarcodeMetricsQr
				{
					Scale = temp.Scale,
					Version = temp.Version,
					EncodeMode = temp.EncodeMode,
					ErrorCorrection = temp.ErrorCorrect,
				};
		}

		/// <summary>
		/// Gets a <see cref="T:BarcodeMetrics"/> object containing the print
		/// metrics needed for printing a barcode of the specified physical
		/// size on a device operating at the specified resolution.
		/// </summary>
		/// <param name="desiredBarcodeDimensions">The desired barcode dimensions in hundredth of an inch.</param>
		/// <param name="printResolution">The print resolution in pixels per inch.</param>
		/// <param name="barcodeCharLength">Length of the barcode in characters.</param>
		/// <returns>
		/// A <see cref="T:Zen.Barcode.BarcodeMetrics"/> object.
		/// </returns>
		public override BarcodeMetrics GetPrintMetrics(
			Size desiredBarcodeDimensions, Size printResolution, int barcodeCharLength)
		{
			return GetDefaultMetrics(30);
		} 
		#endregion

		#region Protected Methods
		/// <summary>
		/// Draws the qr.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="metrics">The metrics.</param>
		/// <returns></returns>
		protected virtual Image DrawQr(string text, BarcodeMetricsQr metrics)
		{
			QRCodeEncoder encoder = new QRCodeEncoder
			{
				Scale = metrics.Scale,
				Version = metrics.Version,
				EncodeMode = metrics.EncodeMode,
				ErrorCorrect = metrics.ErrorCorrection,
			};
			return encoder.Encode(text);
		}
		#endregion
	}

	/// <summary>
	/// <c>BarcodeMetricsQr</c> extends <see cref="BarcodeMetrics2d"/> to
	/// provide configuration properties used to render QR barcodes.
	/// </summary>
	public class BarcodeMetricsQr : BarcodeMetrics2d
	{
		/// <summary>
		/// Gets or sets the version used to render a QR barcode.
		/// </summary>
		/// <value>The version.</value>
		/// <remarks>
		/// The version determines the maximum amount of information that can
		/// be encoded in a QR barcode.
		/// If a value of 0 is used then the most compact representation for a
		/// given piece of text will be used.
		/// If the value is too small for the text to be rendered then an
		/// exception will be thrown during rendering.
		/// </remarks>
		public int Version
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the encoding mode.
		/// </summary>
		/// <value>The encode mode.</value>
		public QrEncodeMode EncodeMode
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the error correction scheme.
		/// </summary>
		/// <value>The error correction scheme.</value>
		public QrErrorCorrection ErrorCorrection
		{
			get;
			set;
		}
	}

	/// <summary>
	/// Defines the QR barcode encoding methods.
	/// </summary>
	public enum QrEncodeMode
	{
		/// <summary>
		/// Suitable for encoding any data.
		/// </summary>
		Byte = 0,

		/// <summary>
		/// Suitable for encoding numeric data.
		/// </summary>
		Numeric = 1,
		
		/// <summary>
		/// Suitable for encoding alpha-numeric data.
		/// </summary>
		AlphaNumeric = 2,
	}

	/// <summary>
	/// Defines the QR barcode error correction schemes.
	/// </summary>
	public enum QrErrorCorrection
	{
		/// <summary>
		/// M error correction.
		/// </summary>
		M = 0,

		/// <summary>
		/// L error correction.
		/// </summary>
		L = 1,

		/// <summary>
		/// H error correction.
		/// </summary>
		H = 2,

		/// <summary>
		/// Q error correction.
		/// </summary>
		Q = 3,
	}
}
