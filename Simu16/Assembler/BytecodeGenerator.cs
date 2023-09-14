using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16.Assembler
{
    public static class BytecodeGenerator
    {
        public static List<byte> Generate(List<ASTNode> nodes, uint origin)
        {
            List<byte> binary = new List<byte>();
            uint offset = origin;

            foreach (ASTNode basenode in nodes)
            {
                if (basenode.Type == AST.Data)
                {
                    ASTData node = (ASTData)basenode;
                    binary.AddRange(node.Data);

                    Debug.Write("[\x1b[96m  >>  ]\x1b[0m Generated data at " + offset.ToString("X4") + "-" + (offset + node.Data.Count - 1).ToString("X4") + ": ");
                    for (int i = 0; i < 24; i++) { if (i < node.Data.Count) { Debug.Write(node.Data[i].ToString("X2") + " "); } }
                    Debug.Write("\n");
                    offset += (uint)node.Data.Count;
                }
                else if (basenode.Type == AST.Instruction)
                {
                    ASTInstruction node = (ASTInstruction)basenode;
                    if (node.Instruction == null) { continue; }
                    binary.Add(node.Instruction.Opcode);

                    if (node.Instruction.Arguments.Length != node.Args.Count) { Debug.Error("Invalid arguments for instruction '" + node.Instruction.Name + "' at line " + node.Line); return binary; }

                    for (int i = 0; i < node.Instruction.Arguments.Length; i++)
                    {
                        if (node.Instruction.Arguments[i] == 'R')
                        {
                            if (node.Args[i].Type != TokenType.LiteralRegister) { Debug.Error("Expected register for argument " + (i + 1) + " of instruction '" + node.Instruction.Name + "' at line " + node.Line); return binary; }
                            binary.Add((byte)(uint.Parse(node.Args[i].Value)));
                        }
                        else if (node.Instruction.Arguments[i] == 'B')
                        {
                            if (node.Args[i].Type == TokenType.LiteralRegister) { Debug.Error("Expected immediate byte for argument " + (i + 1) + " of instruction '" + node.Instruction.Name + "' at line " + node.Line); return binary; }
                            binary.Add((byte)(uint.Parse(node.Args[i].Value)));
                        }
                        else if (node.Instruction.Arguments[i] == 'W')
                        {
                            if (node.Args[i].Type == TokenType.LiteralRegister) { Debug.Error("Expected immediate word for argument " + (i + 1) + " of instruction '" + node.Instruction.Name + "' at line " + node.Line); return binary; }
                            binary.AddRange(BitConverter.GetBytes((ushort)(uint.Parse(node.Args[i].Value))));
                        }
                    }


                    Debug.Write("[\x1b[96m  >>  ]\x1b[0m Generated instruction at " + offset.ToString("X4") + ": " + node.Instruction.Name.PadRight(6, ' '));
                    foreach (Token tok in node.Args) { if (tok.Type == TokenType.LiteralRegister) { Debug.Write("REG("); } else { Debug.Write("IMM("); } Debug.Write(tok.Value + ") "); }
                    Debug.Write("\n");
                    offset += node.Instruction.Bytes;
                }
            }

            Debug.Log("Finished generating binary - Size:" + binary.Count + " bytes");
            return binary;
        }
    }
}
