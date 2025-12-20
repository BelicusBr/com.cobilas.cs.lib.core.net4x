using Cobilas;
using System;
using com.cobilas.cs.lib.core.net4x.test;
using System.Collections.Generic;
using Cobilas.IO.Atlf;

internal class Program
{
	private static void Main(string[] args) {
		//int vl = int.Parse(Console.ReadLine()!);
		//ExceptionMessages.ThrowIfZero(vl);
		//ExceptionMessages.ThrowIfNegative(vl);
		//ExceptionMessages.ThrowIfNegativeOrZero(vl);
		//ExceptionMessages.ThrowIfNegativeOrZero(vl);
		//ExceptionMessages.ThrowIfEqual<int>(vl, 0);
		//ExceptionMessages.ThrowIfNotEqual<int>(vl, 0);
		//ExceptionMessages.ThrowIfGreaterThan<int>(vl, 0);
		//ExceptionMessages.ThrowIfLessThan<int>(vl, 0);
		//Console.WriteLine("Finalizado");
		//foreach (var item in TypeUtilitarian.GetAssemblies())
		//	Console.WriteLine(item.FullName);
		string text =
@"#>Header The use of the header is not mandatory.<#
#! version:/*std:1.0*/
#! encoding:/*utf-8*/

#> Comment <#
#> ATLF format(1.0) <#

#> Uni-line marking <#
#! Tag1:/*value1*/

#> Multi-line marking <#

#!Tag2/Tag33.Tag90_Tag65\Tag22:/*
value1
value2
value3
value4
/*\*/
#\!
\<#
\\
*/
";
		Console.Clear();
		//var lexer = new AtlLexer(text);
		//foreach (var token in lexer.Tokenize())
		//{
		//	WriteLine(token);
		//}
		var lexer = ATLFLexer.Tokenize(text);
		//foreach (Token item in lexer)
		//	WriteLine(item);

		IEnumerator<Token> enumerator = lexer.GetEnumerator();
		while (enumerator.MoveNext()) {
			switch (enumerator.Current.Type) {
				case TokenType.CommentOpen:
					GetComment(enumerator);
					break;
				case TokenType.CommentClose:
					throw new ATLFException("O comentario não foi inisializado!");
				case TokenType.MarkOpen:
					GetTag(enumerator);
					break;
				case TokenType.Colon:
					throw new ATLFException("O bloco de tag não foi inisializado!");
				case TokenType.BlockOpen:
					GetTextBlock(enumerator);
					break;
				case TokenType.BlockClose:
					throw new ATLFException("O bloco de texto não foi inisializado!");
				case TokenType.Identifier:
					throw new ATLFException($"O {TokenType.Identifier} está fora de um bloco de tag!");
				case TokenType.Text:
					throw new ATLFException($"O {TokenType.Text} está fora de um bloco de texto!");
				case TokenType.EndOfFile:
					WriteLine(enumerator.Current);
					break;
			}
		}
	}

	private static void GetTextBlock(IEnumerator<Token> enumerator) {
		do {
			WriteLine(enumerator.Current);
			if (enumerator.Current.Type == TokenType.BlockClose)
				return;
		} while (enumerator.MoveNext());
		throw new ATLFException("O bloco de texto não foi finalizado!");
	}

	private static void GetTag(IEnumerator<Token> enumerator) {
		do {
			WriteLine(enumerator.Current);
			if (enumerator.Current.Type == TokenType.Colon)
				return;
		} while (enumerator.MoveNext());
		throw new ATLFException("O bloco de tag não foi finalizado!");
	}

	private static void GetComment(IEnumerator<Token> enumerator) {
		do {
			WriteLine(enumerator.Current);
			if (enumerator.Current.Type == TokenType.CommentClose)
				return;
		} while (enumerator.MoveNext());
		throw new ATLFException("O comentario não foi finalizado!");
	}

	private static void WriteLine(Token token) {
		switch (token.Type) {
			case TokenType.CommentOpen:
				WriteLine(token.ToString(), ConsoleColor.DarkGreen);
				break;
			case TokenType.CommentClose:
				WriteLine(token.ToString(), ConsoleColor.DarkGreen);
				break;
			case TokenType.MarkOpen:
				WriteLine(token.ToString(), ConsoleColor.DarkYellow);
				break;
			case TokenType.Colon:
				WriteLine(token.ToString(), ConsoleColor.DarkRed);
				break;
			case TokenType.BlockOpen:
				WriteLine(token.ToString(), ConsoleColor.Yellow);
				break;
			case TokenType.BlockClose:
				WriteLine(token.ToString(), ConsoleColor.Yellow);
				break;
			case TokenType.Identifier:
				WriteLine(token.ToString(), ConsoleColor.DarkCyan);
				break;
			default:
				WriteLine(token.ToString(), ConsoleColor.DarkGray);
				break;
		}
	}

	private static void WriteLine(string text, ConsoleColor color) {
		Console.ForegroundColor = color;
		Console.WriteLine(text);
		Console.ResetColor();
	}
}