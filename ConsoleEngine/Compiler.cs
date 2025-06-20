﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine
{
    public class Compiler
    {
        public static bool compilerStarted = false;
        public static int codeI = 0;
        public static string text = null;
        public static int codeValue1 = 0;
        public static int codeValue2 = 0;
        public static void Start(Project project)
        {
            compilerStarted = true;
            Fill.fill.Clear();
            Variables.var.Clear();
            bool render = true;
            bool wait = false;
            project.scene.objX = 0;
            project.scene.objY = 0;
            text = null;
            for (codeI = 0; codeI < project.code.Count; codeI++)
            {
                render = true;
                codeValue1 = 0;
                codeValue2 = 0;
                if (project.code[codeI].IntArg1 > 15999 & project.code[codeI].IntArg1 < 17000)
                {
                    for (int i = 0; Variables.var.Count > i; i++)
                    {
                        if (project.code[codeI].IntArg1 == Variables.var[i].ID)
                        {
                            codeValue1 = project.code[codeI].IntArg1;
                            project.code[codeI].IntArg1 = Variables.var[i].Value;
                            i = 9999;
                        }
                    }
                }
                if (project.code[codeI].IntArg2 > 15999 & project.code[codeI].IntArg2 < 17000)
                {
                    for (int i = 0; Variables.var.Count > i; i++)
                    {
                        if (project.code[codeI].IntArg2 == Variables.var[i].ID)
                        {
                            codeValue2 = project.code[codeI].IntArg2;
                            project.code[codeI].IntArg2 = Variables.var[i].Value;
                            i = 9999;
                        }
                    }
                }
                switch (project.code[codeI].Name)
                {
                    case ("repeat"):
                        int repeatI = codeI;
                        for (int r = 0; r < project.code[repeatI].IntArg1;)
                        {
                            render = true;
                            codeI = repeatI + 1;
                            switch (project.code[repeatI].StrArg1)
                            {
                                case ("moveX"):
                                    project.scene.objX += project.code[codeI].IntArg1;
                                    break;
                                case ("moveY"):
                                    project.scene.objY += project.code[codeI].IntArg1;
                                    break;
                                case ("fill"):
                                    Fill.fill.Add(new(project.scene.objX, project.scene.objY, project.code[codeI].StrArg1, project.code[codeI].StrArg2));
                                    break;
                                case ("fillD"):
                                    Fill.fill.Add(new(project.scene.objX, project.scene.objY, project.code[codeI].StrArg1, project.code[codeI].StrArg2));
                                    break;
                                default:
                                    Text.Error("Неизвестная команда в цикле"); Text.Enter();
                                    break;
                            }
                            codeI = repeatI + 2;
                            switch (project.code[repeatI].StrArg2)
                            {
                                case ("moveX"):
                                    project.scene.objX += project.code[codeI].IntArg1;
                                    break;
                                case ("moveY"):
                                    project.scene.objY += project.code[codeI].IntArg1;
                                    break;
                                case ("fill"):
                                    Fill.fill.Add(new(project.scene.objX, project.scene.objY, project.code[codeI].StrArg1, project.code[codeI].StrArg2));
                                    break;
                                case ("fillD"):
                                    Fill.fill.Add(new(project.scene.objX, project.scene.objY, project.code[codeI].StrArg1, project.code[codeI].StrArg2));
                                    break;
                                default:
                                    Text.Error("Неизвестная команда в цикле"); Text.Enter();
                                    break;
                            }
                            if (project.scene.renderType == "wait")
                            {
                                Text.Enter();
                                project.scene.Render();
                            }
                            if (project.scene.renderType == "default")
                            {
                                project.scene.Render();
                            }
                            r++;
                        }
                        codeI = repeatI + 2;
                        break;
                    case ("pos"):
                        project.scene.objX = project.code[codeI].IntArg1;
                        project.scene.objY = project.code[codeI].IntArg2;
                        break;
                    case ("moveX"):
                        project.scene.objX += project.code[codeI].IntArg1;
                        break;
                    case ("moveY"):
                        project.scene.objY += project.code[codeI].IntArg1;
                        break;
                    case ("move"):
                        project.scene.objX += project.code[codeI].IntArg1;
                        project.scene.objY += project.code[codeI].IntArg2;
                        break;
                    case ("fill"):
                        Fill.fill.Add(new(project.scene.objX, project.scene.objY, project.code[codeI].StrArg1, project.code[codeI].StrArg2));
                        render = false;
                        break;
                    case ("texture"):
                        if (project.code[codeI].StrArg1 == "obj")
                        { project.scene.objTexture = project.code[codeI].StrArg2; }
                        if (project.code[codeI].StrArg1 == "fill")
                        { project.scene.fillTexture = project.code[codeI].StrArg2; }
                        if (project.code[codeI].StrArg1 == "field")
                        { project.scene.fieldTexture = project.code[codeI].StrArg2; }
                        break;
                    case ("colorFG"):
                        if (project.code[codeI].StrArg1 == "obj")
                        { project.scene.objColorFG = project.code[codeI].StrArg2; }
                        if (project.code[codeI].StrArg1 == "field")
                        { project.scene.fieldColorFG = project.code[codeI].StrArg2; }
                        break;
                    case ("colorBG"):
                        if (project.code[codeI].StrArg1 == "obj")
                        { project.scene.objColorBG = project.code[codeI].StrArg2; }
                        if (project.code[codeI].StrArg1 == "field")
                        { project.scene.fieldColorBG = project.code[codeI].StrArg2; }
                        break;
                    case ("wait"):
                        wait = true; render = false;
                        break;
                    case ("clear"):
                        for (int fillI = 0; fillI < Fill.fill.Count; fillI++)
                        {
                            if (project.scene.objX == Fill.fill[fillI].X & project.scene.objY == Fill.fill[fillI].Y)
                            { Fill.fill.RemoveAt(fillI); render = false; }
                        }
                        break;
                    case ("changeRender"):
                        project.scene.renderType = project.code[codeI].StrArg1;
                        render = false;
                        break;
                    case ("fillWithPos"):
                        Fill.fill.Add(new(project.code[codeI].IntArg1, project.code[codeI].IntArg2, project.code[codeI].StrArg1, project.code[codeI].StrArg2));
                        break;
                    case ("sceneSize"):
                        project.scene.X = project.code[codeI].IntArg1;
                        project.scene.Y = project.code[codeI].IntArg2;
                        break;
                    case ("text"):
                        text = project.code[codeI].StrArg1;
                        break;
                    case ("var"):
                        render = false;
                        Variables.var.Add(new(project.code[codeI].StrArg1, project.code[codeI].StrArg2, project.code[codeI].IntArg1, 16000 + Variables.var.Count));
                        break;
                    case ("varSet"):
                        render = false;
                        for (int i = 0; Variables.var.Count > i; i++)
                        {
                            if (project.code[codeI].StrArg1 == Variables.var[i].Name)
                            {
                                Variables.var[i].Value = project.code[codeI].IntArg1;
                                i = 9999;
                            }
                        }
                        break;
                    case ("varChange"):
                        render = false;
                        for (int i = 0; Variables.var.Count > i; i++)
                        {
                            if (project.code[codeI].StrArg1 == Variables.var[i].Name)
                            {
                                if (project.code[codeI].StrArg2 == "+")
                                { Variables.var[i].Value += project.code[codeI].IntArg1; }
                                if (project.code[codeI].StrArg2 == "-")
                                { Variables.var[i].Value -= project.code[codeI].IntArg1; }
                                if (project.code[codeI].StrArg2 == "*")
                                { Variables.var[i].Value *= project.code[codeI].IntArg1; }
                                if (project.code[codeI].StrArg2 == "/")
                                { Variables.var[i].Value /= project.code[codeI].IntArg1; }
                                i = 9999;
                            }
                        }
                        break;
                    case "setVariable":
                        render = false;
                        for (int i = 0; Variables.var.Count > i; i++)
                        {
                            if (project.code[codeI].StrArg1 == Variables.var[i].Name)
                            {
                                if (project.code[codeI].StrArg2 == "+")
                                { Variables.var[i].Value += project.code[codeI].IntArg1; }
                                if (project.code[codeI].StrArg2 == "-")
                                { Variables.var[i].Value -= project.code[codeI].IntArg1; }
                                if (project.code[codeI].StrArg2 == "*")
                                { Variables.var[i].Value *= project.code[codeI].IntArg1; }
                                if (project.code[codeI].StrArg2 == "/")
                                { Variables.var[i].Value /= project.code[codeI].IntArg1; }
                                if (project.code[codeI].StrArg2 == "=")
                                { Variables.var[i].Value = project.code[codeI].IntArg1; }
                                i = 9999;
                            }
                        }
                        break;
                    case ("nRepeat"):
                        repeatI = codeI;
                        int firstCommand = codeI + 1;
                        for (int r = 0; r < project.code[repeatI].IntArg1;)
                        {
                            render = true;
                            for (int nRepeatC = 0; nRepeatC < project.code[repeatI].IntArg2; nRepeatC++)
                            {
                                int codeValueR1 = 0;
                                int codeValueR2 = 0;
                                if (project.code[firstCommand + nRepeatC].IntArg1 > 15999 & project.code[firstCommand + nRepeatC].IntArg1 < 17000)
                                {
                                    for (int i = 0; Variables.var.Count > i; i++)
                                    {
                                        if (project.code[firstCommand + nRepeatC].IntArg1 == Variables.var[i].ID)
                                        {
                                            codeValueR1 = project.code[firstCommand + nRepeatC].IntArg1;
                                            project.code[firstCommand + nRepeatC].IntArg1 = Variables.var[i].Value;
                                            i = 9999;
                                        }
                                    }
                                }
                                if (project.code[firstCommand + nRepeatC].IntArg2 > 15999 & project.code[firstCommand + nRepeatC].IntArg2 < 17000)
                                {
                                    for (int i = 0; Variables.var.Count > i; i++)
                                    {
                                        if (project.code[firstCommand + nRepeatC].IntArg2 == Variables.var[i].ID)
                                        {
                                            codeValueR2 = project.code[firstCommand + nRepeatC].IntArg2;
                                            project.code[firstCommand + nRepeatC].IntArg2 = Variables.var[i].Value;
                                            i = 9999;
                                        }
                                    }
                                }
                                switch (project.code[firstCommand + nRepeatC].Name)
                                {
                                    case ("pos"):
                                        project.scene.objX = project.code[firstCommand + nRepeatC].IntArg1;
                                        project.scene.objY = project.code[firstCommand + nRepeatC].IntArg2;
                                        break;
                                    case ("moveX"):
                                        project.scene.objX += project.code[firstCommand + nRepeatC].IntArg1;
                                        break;
                                    case ("moveY"):
                                        project.scene.objY += project.code[firstCommand + nRepeatC].IntArg1;
                                        break;
                                    case ("move"):
                                        project.scene.objX += project.code[firstCommand + nRepeatC].IntArg1;
                                        project.scene.objY += project.code[firstCommand + nRepeatC].IntArg2;
                                        break;
                                    case ("fill"):
                                        Fill.fill.Add(new(project.scene.objX, project.scene.objY, project.code[firstCommand + nRepeatC].StrArg1, project.code[firstCommand + nRepeatC].StrArg2));
                                        render = false;
                                        break;
                                    case ("texture"):
                                        if (project.code[firstCommand + nRepeatC].StrArg1 == "obj")
                                        { project.scene.objTexture = project.code[firstCommand + nRepeatC].StrArg2; }
                                        if (project.code[firstCommand + nRepeatC].StrArg1 == "fill")
                                        { project.scene.fillTexture = project.code[firstCommand + nRepeatC].StrArg2; }
                                        if (project.code[firstCommand + nRepeatC].StrArg1 == "field")
                                        { project.scene.fieldTexture = project.code[firstCommand + nRepeatC].StrArg2; }
                                        break;
                                    case ("colorFG"):
                                        if (project.code[firstCommand + nRepeatC].StrArg1 == "obj")
                                        { project.scene.objColorFG = project.code[firstCommand + nRepeatC].StrArg2; }
                                        if (project.code[firstCommand + nRepeatC].StrArg1 == "field")
                                        { project.scene.fieldColorFG = project.code[firstCommand + nRepeatC].StrArg2; }
                                        break;
                                    case ("colorBG"):
                                        if (project.code[firstCommand + nRepeatC].StrArg1 == "obj")
                                        { project.scene.objColorBG = project.code[firstCommand + nRepeatC].StrArg2; }
                                        if (project.code[firstCommand + nRepeatC].StrArg1 == "field")
                                        { project.scene.fieldColorBG = project.code[firstCommand + nRepeatC].StrArg2; }
                                        break;
                                    case ("wait"):
                                        wait = true; render = false;
                                        break;
                                    case ("clear"):
                                        for (int fillI = 0; fillI < Fill.fill.Count; fillI++)
                                        {
                                            if (project.scene.objX == Fill.fill[fillI].X & project.scene.objY == Fill.fill[fillI].Y)
                                            { Fill.fill.RemoveAt(fillI); render = false; }
                                        }
                                        break;
                                    case ("changeRender"):
                                        project.scene.renderType = project.code[firstCommand + nRepeatC].StrArg1;
                                        render = false;
                                        break;
                                    case ("fillWithPos"):
                                        Fill.fill.Add(new(project.code[firstCommand + nRepeatC].IntArg1, project.code[firstCommand + nRepeatC].IntArg2, project.code[firstCommand + nRepeatC].StrArg1, project.code[firstCommand + nRepeatC].StrArg2));
                                        break;
                                    case ("sceneSize"):
                                        project.scene.X = project.code[firstCommand + nRepeatC].IntArg1;
                                        project.scene.Y = project.code[firstCommand + nRepeatC].IntArg2;
                                        break;
                                    case ("text"):
                                        text = project.code[firstCommand + nRepeatC].StrArg1;
                                        break;
                                    case ("var"):
                                        Variables.var.Add(new(project.code[firstCommand + nRepeatC].StrArg1, project.code[firstCommand + nRepeatC].StrArg2, project.code[firstCommand + nRepeatC].IntArg1, 16000 + Variables.var.Count));
                                        break;
                                    case ("varSet"):
                                        for (int i = 0; Variables.var.Count > i; i++)
                                        {
                                            if (project.code[firstCommand + nRepeatC].StrArg1 == Variables.var[i].Name)
                                            {
                                                Variables.var[i].Value = project.code[firstCommand + nRepeatC].IntArg1;
                                                i = 9999;
                                            }
                                        }
                                        break;
                                    case ("varChange"):
                                        for (int i = 0; Variables.var.Count > i; i++)
                                        {
                                            if (project.code[firstCommand + nRepeatC].StrArg1 == Variables.var[i].Name)
                                            {
                                                if (project.code[firstCommand + nRepeatC].StrArg2 == "+")
                                                { Variables.var[i].Value += project.code[firstCommand + nRepeatC].IntArg1; }
                                                if (project.code[firstCommand + nRepeatC].StrArg2 == "-")
                                                { Variables.var[i].Value -= project.code[firstCommand + nRepeatC].IntArg1; }
                                                if (project.code[firstCommand + nRepeatC].StrArg2 == "*")
                                                { Variables.var[i].Value *= project.code[firstCommand + nRepeatC].IntArg1; }
                                                if (project.code[firstCommand + nRepeatC].StrArg2 == "/")
                                                { Variables.var[i].Value /= project.code[firstCommand + nRepeatC].IntArg1; }
                                                i = 9999;
                                            }
                                        }
                                        break;
                                    case "setVariable":
                                        for (int i = 0; Variables.var.Count > i; i++)
                                        {
                                            if (project.code[firstCommand + nRepeatC].StrArg1 == Variables.var[i].Name)
                                            {
                                                if (project.code[firstCommand + nRepeatC].StrArg2 == "+")
                                                { Variables.var[i].Value += project.code[firstCommand + nRepeatC].IntArg1; }
                                                if (project.code[firstCommand + nRepeatC].StrArg2 == "-")
                                                { Variables.var[i].Value -= project.code[firstCommand + nRepeatC].IntArg1; }
                                                if (project.code[firstCommand + nRepeatC].StrArg2 == "*")
                                                { Variables.var[i].Value *= project.code[firstCommand + nRepeatC].IntArg1; }
                                                if (project.code[firstCommand + nRepeatC].StrArg2 == "/")
                                                { Variables.var[i].Value /= project.code[firstCommand + nRepeatC].IntArg1; }
                                                if (project.code[firstCommand + nRepeatC].StrArg2 == "=")
                                                { Variables.var[i].Value = project.code[firstCommand + nRepeatC].IntArg1; }
                                                i = 9999;
                                            }
                                        }
                                        break;
                                    default:
                                        Text.Error($"Неизвестная команда в цикле nRepeat"); Text.Enter();
                                        break;
                                }
                                if (codeValueR1 > 15999 & codeValueR1 < 17000)
                                { project.code[firstCommand + nRepeatC].IntArg1 = codeValueR1; }
                                if (codeValueR2 > 15999 & codeValueR2 < 17000)
                                { project.code[firstCommand + nRepeatC].IntArg2 = codeValueR2; }
                            }
                            if (project.scene.renderType == "default")
                            { project.scene.Render(); }
                            if (project.scene.renderType == "wait")
                            { Text.Enter(); project.scene.Render(); }
                            r++;
                        }
                        codeI += project.code[repeatI].IntArg2;
                        break;
                    default:
                        Text.Error($"Неизвестная команда в строке {codeI + 1}"); Text.Enter();
                        break;
                }
                if (codeValue1 > 15999 & codeValue1 < 17000)
                { project.code[codeI].IntArg1 = codeValue1; }
                if (codeValue2 > 15999 & codeValue2 < 17000)
                { project.code[codeI].IntArg2 = codeValue2; }

                Console.BackgroundColor = ConsoleColor.Black;
                if (wait == true)
                { Text.Enter(); wait = false; }

                if (project.scene.renderType == "default")
                { if (render == true) { project.scene.Render(); } }
                if (project.scene.renderType == "wait")
                { Text.Enter(); project.scene.Render(); }
            }
            if (project.scene.renderType == "fast")
            { project.scene.Render(); }

            if (project.scene.renderType != "wait")
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"Fill.Count: [{Fill.fill.Count}]");
                Console.Write(" - Render mode: ");
                if (project.scene.renderType == "default")
                    Console.ForegroundColor = ConsoleColor.White;
                if (project.scene.renderType == "wait")
                    Console.ForegroundColor = ConsoleColor.Yellow;
                if (project.scene.renderType == "fast")
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(project.scene.renderType);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" - Code line: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(codeI);
                Console.WriteLine();
            }
            compilerStarted = false;
            if (project.compilerType == "default" || project.compilerType == "old")
            {
                Editor.CodeView();
                Editor.CodeWrite();
            }
        }
        public static void RunCode()
        {

        }
    }
}