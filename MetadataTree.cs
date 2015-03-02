using System;
using System.Collections.Generic;
using System.IO;

namespace OpenRPG
{
	public struct Source
	{
		public string Filename;
		public int Line;
		public int Indentation;

		public override string ToString() { return "{0}:{1} >{2}".F(Filename, Line, Indentation); }
	}

	public class MetadataNode
	{
		public Source Source;
		public string Key;
		public MetadataTree Tree;

		public MetadataNode(string key, MetadataTree value)
		{
			Key = key;
			Tree = value;
		}

		public MetadataNode(string key, string value, List<MetadataNode> nodes)
			: this(key, new MetadataTree(value, nodes)) { }
	}

	/// <summary>Represents a section definition in a parsed file.</summary>
	public class MetadataTree
	{
		const int SpacesPerLevel = 4;
		public string Value;
		public List<MetadataNode> Nodes;

		public MetadataTree(string value, List<MetadataNode> nodes)
		{
			Value = value;
			Nodes = nodes ?? new List<MetadataNode>();
		}

		public static List<MetadataNode> NodesFromFile(string filename)
		{
			var lines = File.ReadAllLines(filename);

			// List of toplevel nodes
			var ret = new List<List<MetadataNode>>();
			ret.Add(new List<MetadataNode>());

			var lineNum = 0;

			foreach (var l in lines)
			{
				var line = l;
				lineNum++;

				var fullLineCommentIdx = line.IndexOf("**");
				if (fullLineCommentIdx != -1)
					line = line.Substring(0, fullLineCommentIdx).TrimEnd();


				var inlineCommentStart = line.IndexOf("//");
				if (inlineCommentStart != -1)
				{
					var inlineCommentEnd = line.IndexOf(@"\\");
					if (inlineCommentEnd == -1)
						throw new Exception("No inline-comment end token found. Line " + lineNum);

					line = line.SubstringSkip(0, inlineCommentStart, inlineCommentEnd + 2);
				}

				if (line.Length == 0)
					continue;

				var charPos = 0;
				var currChar = line[charPos];
				var indentLevel = 0;
				var spaceCount = 0;
				var isTextStart = false;
				var isEOL = charPos >= line.Length;

				while (!isTextStart && !isEOL)
				{
					if (isEOL = (charPos >= line.Length))
						break;

					currChar = line[charPos];

					switch (currChar)
					{
					case ' ':
						spaceCount++;

						if (spaceCount >= SpacesPerLevel)
						{
							spaceCount = 0;
							indentLevel++;
						}

						charPos++;
						break;

					case '\t':
						throw new Exception("Tab indentations are not yet allowed. {0} ~{1}".F(filename, lineNum));

					case '\n':
					case '\r':
					default:
						isTextStart = true;
						break;
					}
				}

				// The now-unindented line
				var udent = line.Substring(charPos);
				if (string.IsNullOrWhiteSpace(udent))
					continue;

				var location = new Source
				{
					Filename = filename,
					Line = lineNum,
					Indentation = indentLevel
				};

				var rhs = SplitOnDelimiter(ref udent);

				// Remove excess 'dummy' nodes
				while (ret.Count > indentLevel + 1)
					ret.RemoveAt(ret.Count - 1);

				var dummy = new List<MetadataNode>();
				ret[indentLevel].Add(new MetadataNode(udent, rhs, dummy) { Source = location });

				// Without this there is no ret[indentLevel] on the next iteration
				ret.Add(dummy);
			}

			return ret[0];
		}

		/// <summary>Sets 'line' to the left-hand side, and returns the right-hand side.
		/// Neither contain the delimiter char.</summary>
		static string SplitOnDelimiter(ref string lhs)
		{
			var delimIdx = lhs.IndexOf(':');
			if (delimIdx == -1)
				return null;

			var rhs = lhs.Substring(delimIdx).Trim();
			if (rhs.Length == 0)
				return null;

			lhs = lhs.Substring(0, delimIdx).Trim();
			rhs = rhs.Substring(1).Trim();

			return rhs;
		}
	}
}
