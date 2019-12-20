﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FourteenScript : MonoBehaviour
{

    public KMAudio Audio;
    public KMBombInfo bomb;
    public KMBossModule boss;

    public List<KMSelectable> segs;
    public Renderer[] segrends;
    public List<KMSelectable> selectors;
    public Renderer[] selrends;
    public Material[] bcols;
    public TextMesh stagenum;

    private static string[] exempt = null;
    private bool[][] seglist = new bool[36][]{new bool[14] { true, true, false, false, true, true, false, false, true, true, false, false, true, true },     //0
                                              new bool[14] { false, false, false, false, true, true, false, false, false, false, false, false, true, false },//1
                                              new bool[14] { true, false, false, false, false, true, true, true, true, false, false, false, false, true },   //2
                                              new bool[14] { true, false, false, false, false, true, true, true, false, false, false, false, true, true },   //3
                                              new bool[14] { false, true, false, false, false, true, true, true, false, false, false, false, true, false },  //4
                                              new bool[14] { true, false, true, false, false, false, false, true, false, false, false, false, true, true },  //5
                                              new bool[14] { true, true, false, false, false, false, true, true, true, false, false, false, true, true },    //6
                                              new bool[14] { true, false, false, false, true, false, false, false, false, true, false, false, false, false },//7
                                              new bool[14] { true, true, false, false, false, true, true, true, true, false, false, false, true, true },     //8
                                              new bool[14] { true, true, false, false, false, true, true, true, false, false, false, false, true, true },    //9
                                              new bool[14] { true, true, false, false, false, true, true, true, true, false, false, false, true, false },    //A
                                              new bool[14] { true, false, false, true, false, true, false, true, false, false, true, false, true, true },    //B
                                              new bool[14] { true, true, false, false, false, false, false, false, true, false, false, false, false, true }, //C
                                              new bool[14] { true, false, false, true, false, true, false, false, false, false, true, false, true, true },   //D
                                              new bool[14] { true, true, false, false, false, false, true, true, true, false, false, false, false, true },   //E
                                              new bool[14] { true, true, false, false, false, false, true, true, true, false, false, false, false, false },  //F
                                              new bool[14] { true, true, false, false, false, false, false, true, true, false, false, false, true, true },   //G
                                              new bool[14] { false, true, false, false, false, true, true, true, true, false, false, false, true, false },   //H
                                              new bool[14] { true, false, false, true, false, false, false, false, false, false, true, false, false, true }, //I
                                              new bool[14] { false, false, false, false, false, true, false, false, true, false, false, false, true, true }, //J
                                              new bool[14] { false, true, false, false, true, false, true, false, true, false, false, true, false, false },  //K
                                              new bool[14] { false, true, false, false, false, false, false, false, true, false, false, false, false, true },//L
                                              new bool[14] { false, true, true, false, true, true, false, false, true, false, false, false, true, false },   //M
                                              new bool[14] { false, true, true, false, false, true, false, false, true, false, false, true, true, false },   //N
                                              new bool[14] { true, true, false, false, false, true, false, false, true, false, false, false, true, true },   //O
                                              new bool[14] { true, true, false, false, false, true, true, true, true, false, false, false, false, false },   //P
                                              new bool[14] { true, true, false, false, false, true, false, false, true, false, false, true, true, true },    //Q
                                              new bool[14] { true, true, false, false, false, true, true, true, true, false, false, true, false, false },    //R
                                              new bool[14] { true, true, false, false, false, false, true, true, false, false, false, false, true, true },   //S
                                              new bool[14] { true, false, false, true, false, false, false, false, false, false, true, false, false, false },//T
                                              new bool[14] { false, true, false, false, false, true, false, false, true, false, false, false, true, true },  //U
                                              new bool[14] { false, true, false, false, true, false, false, false, true, true, false, false, false, false }, //V
                                              new bool[14] { false, true, false, false, false, true, false, false, true, true, false, true, true, false },   //W
                                              new bool[14] { false, false, true, false, true, false, false, false, false, true, false, true, false, false }, //X
                                              new bool[14] { false, false, true, false, true, false, false, false, false, false, true, false, false, false },//Y
                                              new bool[14] { true, false, false, false, true, false, false, false, false, true, false, false, false, true } };//Z
    private bool[][] segDisplay = new bool[3][];
    private bool[][][] ansDisplay = new bool[2][][] { new bool[14][], new bool[14][] };
    private int buffer;
    private int stageCount;
    private int[] digits = new int[3];
    private int[] dtotals = new int[3];
    private bool pressable;
    private bool[] selection = new bool[3];
    private int selcol = 8;
    private bool refresh;

    private static int moduleIDCounter = 1;
    private int moduleID;
    private bool moduleSolved;

    void Awake()
    {
        moduleID = moduleIDCounter++;
        exempt = GetComponent<KMBossModule>().GetIgnoredModules("14", new string[]
        {
            "Forget Me Not",
            "Forget Everything",
            "Forget This",
            "Forget Infinity",
            "Forget Them All",
            "Simon's Stages",
            "Turn The Key",
            "The Time Keeper",
            "Timing is Everything",
            "Cookie Jars",
            "Purgatory",
            "Hogwarts",
            "Souvenir",
            "The Swan",
            "Divided Squares",
            "The Troll",
            "Tallordered Keys",
            "Forget Enigma",
            "Forget Us Not",
            "Organization",
            "Forget Perspective",
            "The Very Annoying Button",
            "Forget Me Later",
            "14",
            "The Task Master",
            "Simon Supervises",
            "Bad Mouth",
            "Bad TV",
            "Simon Superintends"
        });
        foreach (KMSelectable seg in segs)
        {
            int s = segs.IndexOf(seg);
            if (s < 14)
                segrends[s].material = bcols[8];
            seg.OnInteract += delegate () { Press(false, s); return false; };
        }
        foreach (KMSelectable selector in selectors)
        {
            int s = selectors.IndexOf(selector);
            selrends[s].material = bcols[0];
            selector.OnInteract += delegate () { Press(true, s); return false; };
        }
        GetComponent<KMBombModule>().OnActivate += Activate;
        for (int i = 0; i < 14; i++)
        {
            ansDisplay[0][i] = new bool[3] { false, false, false };
            ansDisplay[1][i] = new bool[3] { false, false, false };
        }
    }

    private void Activate()
    {
        if (bomb.GetSolvableModuleNames().Where(x => !exempt.Contains(x)).Count() == 0)
        {
            moduleSolved = true;
            StartCoroutine(SolveAnim());
        }
        else
            GenerateStage();
    }

    void Update()
    {
        if (!moduleSolved)
        {
            buffer++;
            if (buffer == 9)
            {
                buffer = 0;
                if (stageCount != bomb.GetSolvedModuleNames().Where(x => !exempt.Contains(x)).Count())
                {
                    stageCount++;
                    stagenum.text = stageCount > 99 ? stageCount.ToString() : (stageCount > 9 ? "0" + stageCount.ToString() : "00" + stageCount.ToString());
                    if (stageCount >= bomb.GetSolvableModuleNames().Where(x => !exempt.Contains(x)).Count())
                    {
                        pressable = true;
                        stagenum.text = string.Empty;
                        string[] ansLCDs = new string[14];
                        int[] segcol = new int[14];
                        StartCoroutine(MoveSubmit());
                        selrends[8].material = bcols[8];
                        for (int i = 0; i < 14; i++)
                            segrends[i].material = bcols[8];
                        for (int i = 0; i < 8; i++)
                            selrends[i].material = bcols[i];
                        for (int i = 0; i < 3; i++)
                        {
                            segDisplay[i] = seglist[Mathf.Abs(dtotals[i])];
                            Debug.LogFormat("[14 #{0}] The correct digit for the {1} channel is {2} ({3} {4})", moduleID, "RGB"[i], dtotals[i], dtotals[i] < 0 ? "inverted" : "standard", "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Mathf.Abs(dtotals[i])]);
                        }
                        for (int i = 0; i < 3; i++)
                            for (int j = 0; j < 14; j++)
                            {
                                if (segDisplay[i][j] ^ dtotals[i] < 0)
                                {
                                    ansDisplay[0][j][i] = true;
                                    segcol[j] += (int)Mathf.Pow(2, 2 - i);
                                }
                                ansLCDs[j] = "KBGCRMYW"[segcol[j]].ToString();
                            }
                        string ansGrid = string.Empty;
                        int lcdIndex = 0;
                        for (int i = 0; i < 25; i++)
                        {
                            if (new int[] { 0, 1, 3, 4, 10, 12, 14, 20, 21, 23, 24 }.Contains(i))
                                ansGrid += "-";
                            else
                            {
                                ansGrid += ansLCDs[lcdIndex];
                                lcdIndex++;
                            }
                            if (i % 5 == 4 && i != 24)
                                ansGrid += "\n[14 #" + moduleID + "] ";
                        }
                        Debug.LogFormat("[14 #{0}] The correct submission is:\n[14 #{0}] {1}", moduleID, ansGrid);
                    }
                    else
                        GenerateStage();
                }
            }
        }
    }

    private void Press(bool sel, int s)
    {
        if (pressable && !moduleSolved)
        {
            if (sel)
            {
                if (refresh)
                {
                    refresh = false;
                    for (int i = 0; i < 14; i++)
                    {
                        ansDisplay[1][i] = new bool[3] { false, false, false };
                        segrends[i].material = bcols[8];
                    }
                }
                selectors[s].AddInteractionPunch(0.5f);
                Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
                selcol = s + 8;
                selrends[8].material = bcols[selcol];
                selection = new bool[8][] { new bool[3] { false, false, false }, new bool[3] { false, false, true }, new bool[3] { false, true, false }, new bool[3] { false, true, true }, new bool[3] { true, false, false }, new bool[3] { true, false, true }, new bool[3] { true, true, false }, new bool[3] { true, true, true } }[s];
            }
            else if (!refresh)
            {
                if (s == 14)
                {
                    segs[14].AddInteractionPunch(0.5f);
                    Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
                    bool struck = false;
                    for (int i = 0; i < 14; i++)
                    {
                        if (ansDisplay[0][i][0] == ansDisplay[1][i][0] && ansDisplay[0][i][1] == ansDisplay[1][i][1] && ansDisplay[0][i][2] == ansDisplay[1][i][2])
                            segrends[i].material = bcols[10];
                        else
                        {
                            segrends[i].material = bcols[12];
                            if (!struck)
                            {
                                struck = true;
                                GetComponent<KMBombModule>().HandleStrike();
                            }
                        }
                    }
                    if (!struck)
                    {
                        Audio.PlaySoundAtTransform("InputCorrect", transform);
                        moduleSolved = true;
                        StartCoroutine(SolveAnim());
                    }
                    else
                    {
                        int[] segcol = new int[14];
                        string[] ansLCDs = new string[14];
                        for (int i = 0; i < 3; i++)
                            for (int j = 0; j < 14; j++)
                            {
                                if (ansDisplay[1][j][i])
                                {
                                    segcol[j] += (int)Mathf.Pow(2, 2 - i);
                                }
                                ansLCDs[j] = "KBGCRMYW"[segcol[j]].ToString();
                            }
                        string ansGrid = string.Empty;
                        int lcdIndex = 0;
                        for (int i = 0; i < 25; i++)
                        {
                            if (new int[] { 0, 1, 3, 4, 10, 12, 14, 20, 21, 23, 24 }.Contains(i))
                                ansGrid += "-";
                            else
                            {
                                ansGrid += ansLCDs[lcdIndex];
                                lcdIndex++;
                            }
                            if (i % 5 == 4 && i != 24)
                                ansGrid += "\n[14 #" + moduleID + "] ";
                        }
                        Debug.LogFormat("[14 #{0}] Incorrect Submission:\n[14 #{0}] {1}", moduleID, ansGrid);
                        refresh = true;
                    }
                }
                else
                {
                    Audio.PlaySoundAtTransform("SegSelect" + Random.Range(1, 6).ToString(), transform);
                    ansDisplay[1][s] = selection;
                    segrends[s].material = bcols[selcol];
                }
            }
        }
    }

    private void GenerateStage()
    {
        int function = Random.Range(0, 8);
        for (int i = 0; i < 3; i++)
        {
            digits[i] = Random.Range(-35, 36);
            switch (function)
            {
                case 0:
                    Debug.LogFormat("[14 #{0}] At stage {1}, The digit shown on the {2} channel was {3} {5} ({4}). The K function outputs {6} - {4} = {7} ({8})", moduleID, stageCount, "RGB"[i], digits[i] < 0 ? "inverted" : "standard", digits[i], "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Mathf.Abs(digits[i])], dtotals[i], (-digits[i] + dtotals[i]) % 36, ((-digits[i] + dtotals[i]) % 36 < 0 ? "inverted " : "standard ") + "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Mathf.Abs((-digits[i] + dtotals[i]) % 36)]);
                    dtotals[i] -= digits[i];
                    dtotals[i] %= 36;
                    break;
                case 1:
                    Debug.LogFormat("[14 #{0}] At stage {1}, The digit shown on the {2} channel was {3} {5} ({4}). The B function outputs 2*{6} + {4} = {7} ({8})", moduleID, stageCount, "RGB"[i], digits[i] < 0 ? "inverted" : "standard", digits[i], "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Mathf.Abs(digits[i])], dtotals[i], (digits[i] + dtotals[i] * 2) % 36, ((digits[i] + dtotals[i] * 2) % 36 < 0 ? "inverted " : "standard ") + "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Mathf.Abs((digits[i] + dtotals[i] * 2) % 36)]);
                    dtotals[i] *= 2;
                    dtotals[i] += digits[i];
                    dtotals[i] %= 36;
                    break;
                case 2:
                    Debug.LogFormat("[14 #{0}] At stage {1}, The digit shown on the {2} channel was {3} {5} ({4}). The G function outputs {6} + {4} = {7} ({8})", moduleID, stageCount, "RGB"[i], digits[i] < 0 ? "inverted" : "standard", digits[i], "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Mathf.Abs(digits[i])], dtotals[i], (Mathf.Abs(digits[i]) + dtotals[i]) % 36, ((Mathf.Abs(digits[i]) + dtotals[i]) % 36 < 0 ? "inverted " : "standard ") + "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Mathf.Abs((Mathf.Abs(digits[i]) + dtotals[i]) % 36)]);
                    dtotals[i] += Mathf.Abs(digits[i]);
                    dtotals[i] %= 36;
                    break;
                case 3:
                    Debug.LogFormat("[14 #{0}] At stage {1}, The digit shown on the {2} channel was {3} {5} ({4}). The C function outputs {6} + {9}{4} = {7} ({8})", moduleID, stageCount, "RGB"[i], digits[i] < 0 ? "inverted" : "standard", digits[i], "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Mathf.Abs(digits[i])], dtotals[i], (((digits[i] < 0 ? 1 : 2) * digits[i]) + dtotals[i]) % 36, ((((digits[i] < 0 ? 1 : 2) * digits[i]) + dtotals[i]) % 36 < 0 ? "inverted " : "standard ") + "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Mathf.Abs((((digits[i] < 0 ? 1 : 2) * digits[i]) + dtotals[i]) % 36)], digits[i] < 0 ? "" : "2*");
                    dtotals[i] += digits[i] < 0 ? digits[i] : digits[i] * 2;
                    dtotals[i] %= 36;
                    break;
                case 4:
                    Debug.LogFormat("[14 #{0}] At stage {1}, The digit shown on the {2} channel was {3} {5} ({4}). The R function outputs {6} + 2*{4} = {7} ({8})", moduleID, stageCount, "RGB"[i], digits[i] < 0 ? "inverted" : "standard", digits[i], "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Mathf.Abs(digits[i])], dtotals[i], (digits[i] * 2 + dtotals[i]) % 36, ((digits[i] * 2 + dtotals[i]) % 36 < 0 ? "inverted " : "standard ") + "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Mathf.Abs((digits[i] * 2 + dtotals[i]) % 36)]);
                    dtotals[i] += digits[i] * 2;
                    dtotals[i] %= 36;
                    break;
                case 5:
                    Debug.LogFormat("[14 #{0}] At stage {1}, The digit shown on the {2} channel was {3} {5} ({4}). The M function outputs {6} - {4} = {7} ({8})", moduleID, stageCount, "RGB"[i], digits[i] < 0 ? "inverted" : "standard", digits[i], "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Mathf.Abs(digits[i])], dtotals[i], (-Mathf.Abs(digits[i]) + dtotals[i]) % 36, ((-Mathf.Abs(digits[i]) + dtotals[i]) % 36 < 0 ? "inverted " : "standard ") + "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Mathf.Abs((-Mathf.Abs(digits[i]) + dtotals[i]) % 36)]);
                    dtotals[i] -= Mathf.Abs(digits[i]);
                    dtotals[i] %= 36;
                    break;
                case 6:
                    Debug.LogFormat("[14 #{0}] At stage {1}, The digit shown on the {2} channel was {3} {5} ({4}). The Y function outputs {6} + {9}{4} = {7} ({8})", moduleID, stageCount, "RGB"[i], digits[i] < 0 ? "inverted" : "standard", digits[i], "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Mathf.Abs(digits[i])], dtotals[i], (((digits[i] < 0 ? 2 : 1) * digits[i]) + dtotals[i]) % 36, ((((digits[i] < 0 ? 2 : 1) * digits[i]) + dtotals[i]) % 36 < 0 ? "inverted " : "standard ") + "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Mathf.Abs((((digits[i] < 0 ? 2 : 1) * digits[i]) + dtotals[i]) % 36)], digits[i] < 0 ? "2*" : "");
                    dtotals[i] += digits[i] < 0 ? digits[i] * 2 : digits[i];
                    dtotals[i] %= 36;
                    break;
                default:
                    Debug.LogFormat("[14 #{0}] At stage {1}, The digit shown on the {2} channel was {3} {5} ({4}). The W function outputs {6} + {4} = {7} ({8})", moduleID, stageCount, "RGB"[i], digits[i] < 0 ? "inverted" : "standard", digits[i], "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Mathf.Abs(digits[i])], dtotals[i], (digits[i] + dtotals[i]) % 36, ((digits[i] + dtotals[i]) % 36 < 0 ? "inverted " : "standard ") + "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Mathf.Abs((digits[i] + dtotals[i]) % 36)]);
                    dtotals[i] += digits[i];
                    dtotals[i] %= 36;
                    break;
            }
            selrends[8].material = bcols[function + 8];
            segDisplay[i] = seglist[Mathf.Abs(digits[i])];
        }
        int[] segcol = new int[14];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 14; j++)
            {
                if (segDisplay[i][j] ^ digits[i] < 0)
                    segcol[j] += (int)Mathf.Pow(2, 2 - i);
                segrends[j].material = bcols[segcol[j] + 8];
            }
        }
    }

    private IEnumerator MoveSubmit()
    {
        for (int i = 0; i < 10; i++)
        {
            segs[14].transform.localPosition += new Vector3(0, 0.1f, 0);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator SolveAnim()
    {
        int[] solved = new int[6] { 28, 24, 21, 31, 14, 13 };
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 14; j++)
            {
                if (i != 6)
                {
                    if (seglist[solved[i]][j])
                        segrends[j].material = bcols[10];
                    else
                        segrends[j].material = bcols[8];
                }
                else
                    segrends[j].material = bcols[8];
            }
            yield return new WaitForSeconds(0.5f);
        }
        for (int i = 0; i < 14; i++)
            segrends[i].material = bcols[8];
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
        GetComponent<KMBombModule>().HandlePass();
    }

#pragma warning disable 414
    private string TwitchHelpMessage = "!{0} KRGBCMYW [Selects colour] | !{0} 1-14 [Changes segment (in reading order) to selected colour] | !{0} submit | Colouring commands can be chained, separated with spaces e.g. R 1 2 G 12 13";
#pragma warning restore 414
    IEnumerator ProcessTwitchCommand(string command)
    {

        if (command.ToLowerInvariant() == "submit")
        {
            yield return null;
            segs[14].OnInteract();
        }
        else
        {
            string[] interactions = command.ToUpperInvariant().Split(' ');
            foreach (string i in interactions)
                if (!new string[] { "K", "R", "G", "B", "C", "M", "Y", "W", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14" }.Contains(i))
                {
                    yield return "sendtochaterror Invalid command: " + i;
                    yield break;
                }
            for (int i = 0; i < interactions.Length; i++)
            {
                yield return null;
                int selindex = "KBGCRMYW".IndexOf(interactions[i]);
                if (selindex > -1)
                    selectors[selindex].OnInteract();
                else
                {
                    int segindex = 0;
                    int.TryParse(interactions[i], out segindex);
                    segs[segindex - 1].OnInteract();
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
    }

    private IEnumerator TwitchHandleForcedSolve()
    {
        if (pressable)
        {
            bool[][] truth = new bool[8][] { new bool[3] { false, false, false }, new bool[3] { false, false, true }, new bool[3] { false, true, false }, new bool[3] { false, true, true }, new bool[3] { true, false, false }, new bool[3] { true, false, true }, new bool[3] { true, true, false }, new bool[3] { true, true, true } };
            for (int i = 0; i < 8; i++)
            {
                yield return null;
                selectors[i].OnInteract();
                yield return new WaitForSeconds(0.1f);
                for (int j = 0; j < 14; j++)
                {
                    if (truth[i][0] == ansDisplay[0][j][0] && truth[i][1] == ansDisplay[0][j][0] && truth[i][2] == ansDisplay[0][j][2])
                    {
                        yield return null;
                        segs[j].OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                }
            }
            yield return null;
            segs[14].OnInteract();
        }
    }
}
