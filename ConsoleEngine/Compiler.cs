using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine
{
    public class Compiler
    {
        public static void Start(Project project )
        {
            Fill.fill.Clear();
            bool wait = false;

            for (int codeI = 0; codeI < project.code.Count; codeI++)
            {
                switch (project.code[codeI].Name)
                {
                    case ("repeat"):
                        int repeatI = codeI;
                        for (int r = 0; r < project.code[repeatI].IntArg1;)
                        {
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
                            }
                            if (project.scene.renderType == "wait")
                            {
                                Console.WriteLine("Нажмите 'enter'");
                                Console.ReadLine();
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
                    case ("fill"):
                        Fill.fill.Add(new(project.scene.objX, project.scene.objY, project.code[codeI].StrArg1, project.code[codeI].StrArg2));
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
                        wait = true;
                        break;
                    case ("clear"):
                        for (int fillI = 0; fillI < Fill.fill.Count; fillI++)
                        {
                            if (project.scene.objX == Fill.fill[fillI].X & project.scene.objY == Fill.fill[fillI].Y)
                            {
                                Fill.fill.RemoveAt(fillI);
                            }
                        }
                        break;
                    case ("changeRender"):
                        project.scene.renderType = project.code[codeI].StrArg1;
                        break;
                    case ("fillWithPos"):
                        Fill.fill.Add(new(project.code[codeI].IntArg1, project.code[codeI].IntArg2, project.code[codeI].StrArg1, project.code[codeI].StrArg2));
                        break;
                    case ("sceneSize"):
                        project.scene.X = project.code[codeI].IntArg1;
                        project.scene.Y = project.code[codeI].IntArg2;
                        break;
                    default:
                        codeI = 999;
                        Engine.Error("Ошибка компилятора");
                        break;
                }
                if (wait == true)
                {
                    Console.WriteLine("Нажмите 'Enter'");
                    Console.ReadLine();
                    wait = false;
                }

                if (project.scene.renderType == "default")
                {
                    project.scene.Render();
                }
                if (project.scene.renderType == "wait")
                {
                    Console.WriteLine("Нажмите 'Enter'");
                    Console.ReadLine();
                    project.scene.Render();
                }
            }
            if (project.scene.renderType == "fast")
            {
                project.scene.Render();
            }
            Console.ForegroundColor = ConsoleColor.Black;
            Editor.CodeView();
            Editor.CodeWrite();
        }
    }
}
