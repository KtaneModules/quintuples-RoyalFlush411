using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class quintuplesScript : MonoBehaviour
{
    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMSelectable submitButton;
    public KMSelectable[] upCycleButtons;
    public KMSelectable[] downCycleButtons;

    public TextMesh[] inputNumbers;
    public int[] displayedInputNumbers;

    public int[] cyclingNumbers;
    public TextMesh[] cyclingNumbersDisplay;

    public Color[] potentialColors;
    public string[] potentialColorsName;
    public Color[] chosenColors;
    public string[] chosenColorsName;
    public int[] indexModifier;
    private int lastSelectedNumber = 0;
    public int[] numberOfEachColour = new int[5];

    public Color[] answerColors;

    public int[] cyclingNumbersModified = new int[25];
    private int[] answers = new int[5];

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    void Awake()
    {
        moduleId = moduleIdCounter++;
        submitButton.OnInteract += delegate () { OnSubmitButton(); return false; };
        foreach (KMSelectable iterator in upCycleButtons)
        {
            KMSelectable pressedButton = iterator;
            iterator.OnInteract += delegate () { OnUpCycleButton(pressedButton); return false; };
        }
        foreach (KMSelectable iterator in downCycleButtons)
        {
            KMSelectable pressedButton = iterator;
            iterator.OnInteract += delegate () { OnDownCycleButton(pressedButton); return false; };
        }
    }

    void Start()
    {
        for (int i = 0; i <= 4; i++)
        {
            inputNumbers[i].text = displayedInputNumbers[i].ToString();
        }
        PickNumbers();
    }

    void PickNumbers()
    {
        for (int i = 0; i <= 24; i++)
        {
            int index = UnityEngine.Random.Range(1, 11);
            while (index == lastSelectedNumber)
            {
                index = UnityEngine.Random.Range(1, 11);
            }
            lastSelectedNumber = index;
            cyclingNumbers[i] = index;
            cyclingNumbersModified[i] = index;
            int colorIndex = UnityEngine.Random.Range(0, 5);
            numberOfEachColour[colorIndex]++;
            chosenColors[i] = potentialColors[colorIndex];
            chosenColorsName[i] = potentialColorsName[colorIndex];
        }
        Logic();
        StartCoroutine(display0());
        StartCoroutine(display1());
        StartCoroutine(display2());
        StartCoroutine(display3());
        StartCoroutine(display4());
    }

    private void Logic()
    {
        Debug.LogFormat("[Quintuples #{0}] Position 1 numbers: {1}{2}{3}{4}{5}. Position 1 colours: {6}, {7}, {8}, {9}, {10}.", moduleId, cyclingNumbers[0] % 10, cyclingNumbers[1] % 10, cyclingNumbers[2] % 10, cyclingNumbers[3] % 10, cyclingNumbers[4] % 10, chosenColorsName[0], chosenColorsName[1], chosenColorsName[2], chosenColorsName[3], chosenColorsName[4]);
        Debug.LogFormat("[Quintuples #{0}] Position 2 numbers: {1}{2}{3}{4}{5}. Position 2 colours: {6}, {7}, {8}, {9}, {10}.", moduleId, cyclingNumbers[5] % 10, cyclingNumbers[6] % 10, cyclingNumbers[7] % 10, cyclingNumbers[8] % 10, cyclingNumbers[9] % 10, chosenColorsName[5], chosenColorsName[6], chosenColorsName[7], chosenColorsName[8], chosenColorsName[9]);
        Debug.LogFormat("[Quintuples #{0}] Position 3 numbers: {1}{2}{3}{4}{5}. Position 3 colours: {6}, {7}, {8}, {9}, {10}.", moduleId, cyclingNumbers[10] % 10, cyclingNumbers[11] % 10, cyclingNumbers[12] % 10, cyclingNumbers[13] % 10, cyclingNumbers[14] % 10, chosenColorsName[10], chosenColorsName[11], chosenColorsName[12], chosenColorsName[13], chosenColorsName[14]);
        Debug.LogFormat("[Quintuples #{0}] Position 4 numbers: {1}{2}{3}{4}{5}. Position 4 colours: {6}, {7}, {8}, {9}, {10}.", moduleId, cyclingNumbers[15] % 10, cyclingNumbers[16] % 10, cyclingNumbers[17] % 10, cyclingNumbers[18] % 10, cyclingNumbers[19] % 10, chosenColorsName[15], chosenColorsName[16], chosenColorsName[17], chosenColorsName[18], chosenColorsName[19]);
        Debug.LogFormat("[Quintuples #{0}] Position 5 numbers: {1}{2}{3}{4}{5}. Position 5 colours: {6}, {7}, {8}, {9}, {10}.", moduleId, cyclingNumbers[20] % 10, cyclingNumbers[21] % 10, cyclingNumbers[22] % 10, cyclingNumbers[23] % 10, cyclingNumbers[24] % 10, chosenColorsName[20], chosenColorsName[21], chosenColorsName[22], chosenColorsName[23], chosenColorsName[24]);
        Debug.LogFormat("[Quintuples #{0}] There are {1} red flashes.", moduleId, numberOfEachColour[0]);
        Debug.LogFormat("[Quintuples #{0}] There are {1} blue flashes.", moduleId, numberOfEachColour[1]);
        Debug.LogFormat("[Quintuples #{0}] There are {1} orange flashes.", moduleId, numberOfEachColour[2]);
        Debug.LogFormat("[Quintuples #{0}] There are {1} green flashes.", moduleId, numberOfEachColour[3]);
        Debug.LogFormat("[Quintuples #{0}] There are {1} pink flashes.", moduleId, numberOfEachColour[4]);

        if (chosenColors[0] == potentialColors[0] || chosenColors[0] == potentialColors[2])
        {
            cyclingNumbersModified[0] += 7;
            Debug.LogFormat("[Quintuples #{0}] Iteration 1, position 1 ({1}) is affected. +7.", moduleId, cyclingNumbers[0]);
        }
        if (chosenColors[5] == potentialColors[1])
        {
            cyclingNumbersModified[5] += 13;
            Debug.LogFormat("[Quintuples #{0}] Iteration 1, position 2 ({1}) is affected. +13.", moduleId, cyclingNumbers[5]);
        }
        if (chosenColors[10] == potentialColors[4])
        {
            cyclingNumbersModified[10] *= 2;
            Debug.LogFormat("[Quintuples #{0}] Iteration 1, position 3 ({1}) is affected. x2.", moduleId, cyclingNumbers[10]);
        }
        if (chosenColors[15] == potentialColors[3])
        {
            cyclingNumbersModified[15] *= 3;
            Debug.LogFormat("[Quintuples #{0}] Iteration 1, position 4 ({1}) is affected. x3.", moduleId, cyclingNumbers[15]);
        }
        if (chosenColors[20] == potentialColors[2] || chosenColors[20] == potentialColors[1])
        {
            cyclingNumbersModified[20] /= 2;
            Debug.LogFormat("[Quintuples #{0}] Iteration 1, position 5 ({1}) is affected. Half and round down.", moduleId, cyclingNumbers[20]);
        }

        if (chosenColors[1] == potentialColors[1])
        {
            cyclingNumbersModified[1] += 7;
            Debug.LogFormat("[Quintuples #{0}] Iteration 2, position 1 ({1}) is affected. +7.", moduleId, cyclingNumbers[1]);
        }
        if (chosenColors[6] == potentialColors[4] || chosenColors[6] == potentialColors[0])
        {
            cyclingNumbersModified[6] += 13;
            Debug.LogFormat("[Quintuples #{0}] Iteration 2, position 2 ({1}) is affected. +13.", moduleId, cyclingNumbers[6]);
        }
        if (chosenColors[11] == potentialColors[2])
        {
            cyclingNumbersModified[11] *= 2;
            Debug.LogFormat("[Quintuples #{0}] Iteration 2, position 3 ({1}) is affected. x2.", moduleId, cyclingNumbers[11]);
        }
        if (chosenColors[16] == potentialColors[0])
        {
            cyclingNumbersModified[16] *= 3;
            Debug.LogFormat("[Quintuples #{0}] Iteration 2, position 4 ({1}) is affected. x3.", moduleId, cyclingNumbers[16]);
        }
        if (chosenColors[21] == potentialColors[3] || chosenColors[21] == potentialColors[4])
        {
            cyclingNumbersModified[21] /= 2;
            Debug.LogFormat("[Quintuples #{0}] Iteration 2, position 5 ({1}) is affected. Half and round down.", moduleId, cyclingNumbers[21]);
        }

        if (chosenColors[2] == potentialColors[2])
        {
            cyclingNumbersModified[2] += 7;
            Debug.LogFormat("[Quintuples #{0}] Iteration 3, position 1 ({1}) is affected. +7.", moduleId, cyclingNumbers[2]);
        }
        if (chosenColors[7] == potentialColors[0])
        {
            cyclingNumbersModified[7] += 13;
            Debug.LogFormat("[Quintuples #{0}] Iteration 3, position 2 ({1}) is affected. +13.", moduleId, cyclingNumbers[7]);
        }
        if (chosenColors[12] == potentialColors[3] || chosenColors[12] == potentialColors[2])
        {
            cyclingNumbersModified[12] *= 2;
            Debug.LogFormat("[Quintuples #{0}] Iteration 3, position 3 ({1}) is affected. x2.", moduleId, cyclingNumbers[12]);
        }
        if (chosenColors[17] == potentialColors[1] || chosenColors[17] == potentialColors[3])
        {
            cyclingNumbersModified[17] *= 3;
            Debug.LogFormat("[Quintuples #{0}] Iteration 3, position 4 ({1}) is affected. x3.", moduleId, cyclingNumbers[17]);
        }
        if (chosenColors[22] == potentialColors[4])
        {
            cyclingNumbersModified[22] /= 2;
            Debug.LogFormat("[Quintuples #{0}] Iteration 3, position 5 ({1}) is affected. Half and round down.", moduleId, cyclingNumbers[22]);
        }

        if (chosenColors[3] == potentialColors[3])
        {
            cyclingNumbersModified[3] += 7;
            Debug.LogFormat("[Quintuples #{0}] Iteration 4, position 1 ({1}) is affected. +7.", moduleId, cyclingNumbers[3]);
        }
        if (chosenColors[8] == potentialColors[2] || chosenColors[8] == potentialColors[4])
        {
            cyclingNumbersModified[8] += 13;
            Debug.LogFormat("[Quintuples #{0}] Iteration 4, position 2 ({1}) is affected. +13.", moduleId, cyclingNumbers[8]);
        }
        if (chosenColors[13] == potentialColors[1] || chosenColors[13] == potentialColors[3])
        {
            cyclingNumbersModified[13] *= 2;
            Debug.LogFormat("[Quintuples #{0}] Iteration 4, position 3 ({1}) is affected. x2.", moduleId, cyclingNumbers[13]);
        }
        if (chosenColors[18] == potentialColors[4])
        {
            cyclingNumbersModified[18] *= 3;
            Debug.LogFormat("[Quintuples #{0}] Iteration 4, position 4 ({1}) is affected. x3.", moduleId, cyclingNumbers[18]);
        }
        if (chosenColors[23] == potentialColors[0])
        {
            cyclingNumbersModified[23] /= 2;
            Debug.LogFormat("[Quintuples #{0}] Iteration 4, position 5 ({1}) is affected. Half and round down.", moduleId, cyclingNumbers[23]);
        }

        if (chosenColors[4] == potentialColors[4] || chosenColors[4] == potentialColors[1])
        {
            cyclingNumbersModified[4] += 7;
            Debug.LogFormat("[Quintuples #{0}] Iteration 5, position 1 ({1}) is affected. +7.", moduleId, cyclingNumbers[4]);
        }
        if (chosenColors[9] == potentialColors[3])
        {
            cyclingNumbersModified[9] += 13;
            Debug.LogFormat("[Quintuples #{0}] Iteration 5, position 2 ({1}) is affected. +13.", moduleId, cyclingNumbers[9]);
        }
        if (chosenColors[14] == potentialColors[0])
        {
            cyclingNumbersModified[14] *= 2;
            Debug.LogFormat("[Quintuples #{0}] Iteration 5, position 3 ({1}) is affected. x2.", moduleId, cyclingNumbers[14]);
        }
        if (chosenColors[19] == potentialColors[2] || chosenColors[19] == potentialColors[0])
        {
            cyclingNumbersModified[19] *= 3;
            Debug.LogFormat("[Quintuples #{0}] Iteration 5, position 4 ({1}) is affected. x3.", moduleId, cyclingNumbers[19]);
        }
        if (chosenColors[24] == potentialColors[1])
        {
            cyclingNumbersModified[24] /= 2;
            Debug.LogFormat("[Quintuples #{0}] Iteration 5, position 5 ({1}) is affected. Half and round down.", moduleId, cyclingNumbers[24]);
        }

        answers[0] = (((cyclingNumbersModified[0] + cyclingNumbersModified[5] + cyclingNumbersModified[10] + cyclingNumbersModified[15] + cyclingNumbersModified[20]) % (numberOfEachColour[0] + numberOfEachColour[2])) % 10);
        answers[1] = (((cyclingNumbersModified[1] + cyclingNumbersModified[6] + cyclingNumbersModified[11] + cyclingNumbersModified[16] + cyclingNumbersModified[21]) % (numberOfEachColour[1] + numberOfEachColour[4])) % 10);
        answers[2] = (((cyclingNumbersModified[2] + cyclingNumbersModified[7] + cyclingNumbersModified[12] + cyclingNumbersModified[17] + cyclingNumbersModified[22]) % (numberOfEachColour[0] + numberOfEachColour[3])) % 10);
        answers[3] = (((cyclingNumbersModified[3] + cyclingNumbersModified[8] + cyclingNumbersModified[13] + cyclingNumbersModified[18] + cyclingNumbersModified[23]) % (numberOfEachColour[1] + numberOfEachColour[2])) % 10);
        answers[4] = (((((cyclingNumbersModified[4] + cyclingNumbersModified[9] + cyclingNumbersModified[14] + cyclingNumbersModified[19] + cyclingNumbersModified[24]) / 10) % 10) + (numberOfEachColour[3] + numberOfEachColour[4])) % 10);
        Debug.LogFormat("[Quintuples #{0}] The sum of the modified first iteration numbers ({1}), modulo ({2} + {3}), modulo 10 is {4}.", moduleId, cyclingNumbersModified[0] + cyclingNumbersModified[5] + cyclingNumbersModified[10] + cyclingNumbersModified[15] + cyclingNumbersModified[20], numberOfEachColour[0], numberOfEachColour[2], answers[0]);
        Debug.LogFormat("[Quintuples #{0}] The sum of the modified second iteration numbers ({1}), modulo ({2} + {3}) is {4}.", moduleId, cyclingNumbersModified[1] + cyclingNumbersModified[6] + cyclingNumbersModified[11] + cyclingNumbersModified[16] + cyclingNumbersModified[21], numberOfEachColour[1], numberOfEachColour[4], answers[1]);
        Debug.LogFormat("[Quintuples #{0}] The sum of the modified third iteration numbers ({1}), modulo ({2} + {3}), modulo 10 is {4}.", moduleId, cyclingNumbersModified[2] + cyclingNumbersModified[7] + cyclingNumbersModified[12] + cyclingNumbersModified[17] + cyclingNumbersModified[22], numberOfEachColour[0], numberOfEachColour[3], answers[2]);
        Debug.LogFormat("[Quintuples #{0}] The sum of the modified fourth iteration numbers ({1}), modulo ({2} + {3}), is {4}.", moduleId, cyclingNumbersModified[3] + cyclingNumbersModified[8] + cyclingNumbersModified[13] + cyclingNumbersModified[18] + cyclingNumbersModified[23], numberOfEachColour[1], numberOfEachColour[2], answers[3]);
        Debug.LogFormat("[Quintuples #{0}] The tens column of the sum of the modified fifth iteration numbers ({1}), + ({2} + {3}), modulo 10 is {4}.", moduleId, (((cyclingNumbersModified[4] + cyclingNumbersModified[9] + cyclingNumbersModified[14] + cyclingNumbersModified[19] + cyclingNumbersModified[24]) / 10) % 10), numberOfEachColour[3], numberOfEachColour[4], answers[4]);
        Debug.LogFormat("[Quintuples #{0}] The correct number to input is {1}{2}{3}{4}{5}.", moduleId, answers[0], answers[1], answers[2], answers[3], answers[4]);
    }

    IEnumerator display0()
    {
        int i0 = 0;
        while (!moduleSolved)
        {
            cyclingNumbersDisplay[0].text = ((cyclingNumbers[i0 + indexModifier[0]]) % 10).ToString();
            cyclingNumbersDisplay[0].color = chosenColors[i0 + indexModifier[0]];
            float delay = UnityEngine.Random.Range(0.25f, 2f);
            yield return new WaitForSeconds(delay);
            i0++;
            if (i0 == 5)
            {
                i0 = 0;
                cyclingNumbersDisplay[0].text = "";
                yield return new WaitForSeconds(1f);
            }
        }
        cyclingNumbersDisplay[0].text = "";
    }

    IEnumerator display1()
    {
        int i1 = 0;
        while (!moduleSolved)
        {
            cyclingNumbersDisplay[1].text = ((cyclingNumbers[i1 + indexModifier[1]]) % 10).ToString();
            cyclingNumbersDisplay[1].color = chosenColors[i1 + indexModifier[1]];
            float delay = UnityEngine.Random.Range(0.25f, 2f);
            yield return new WaitForSeconds(delay);
            i1++;
            if (i1 == 5)
            {
                i1 = 0;
                cyclingNumbersDisplay[1].text = "";
                yield return new WaitForSeconds(1f);
            }
        }
        cyclingNumbersDisplay[1].text = "";
    }

    IEnumerator display2()
    {
        int i2 = 0;
        while (!moduleSolved)
        {
            cyclingNumbersDisplay[2].text = ((cyclingNumbers[i2 + indexModifier[2]]) % 10).ToString();
            cyclingNumbersDisplay[2].color = chosenColors[i2 + indexModifier[2]];
            float delay = UnityEngine.Random.Range(0.25f, 2f);
            yield return new WaitForSeconds(delay);
            i2++;
            if (i2 == 5)
            {
                i2 = 0;
                cyclingNumbersDisplay[2].text = "";
                yield return new WaitForSeconds(1f);
            }
        }
        cyclingNumbersDisplay[2].text = "";
    }

    IEnumerator display3()
    {
        int i3 = 0;
        while (!moduleSolved)
        {
            cyclingNumbersDisplay[3].text = ((cyclingNumbers[i3 + indexModifier[3]]) % 10).ToString();
            cyclingNumbersDisplay[3].color = chosenColors[i3 + indexModifier[3]];
            float delay = UnityEngine.Random.Range(0.25f, 2f);
            yield return new WaitForSeconds(delay);
            i3++;
            if (i3 == 5)
            {
                i3 = 0;
                cyclingNumbersDisplay[3].text = "";
                yield return new WaitForSeconds(1f);
            }
        }
        cyclingNumbersDisplay[3].text = "";
    }

    IEnumerator display4()
    {
        int i4 = 0;
        while (!moduleSolved)
        {
            cyclingNumbersDisplay[4].text = ((cyclingNumbers[i4 + indexModifier[4]]) % 10).ToString();
            cyclingNumbersDisplay[4].color = chosenColors[i4 + indexModifier[4]];
            float delay = UnityEngine.Random.Range(0.25f, 2f);
            yield return new WaitForSeconds(delay);
            i4++;
            if (i4 == 5)
            {
                i4 = 0;
                cyclingNumbersDisplay[4].text = "";
                yield return new WaitForSeconds(1f);
            }
        }
        cyclingNumbersDisplay[4].text = "";
    }

    public void OnSubmitButton()
    {
        submitButton.AddInteractionPunch();
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        if (moduleSolved)
        {
            return;
        }
        if (inputNumbers[0].text == answers[0].ToString() && inputNumbers[1].text == answers[1].ToString() && inputNumbers[2].text == answers[2].ToString() && inputNumbers[3].text == answers[3].ToString() && inputNumbers[4].text == answers[4].ToString())
        {
            Debug.LogFormat("[Quintuples #{0}] You entered {1}{2}{3}{4}{5}. That is correct. Module disarmed.", moduleId, inputNumbers[0].text, inputNumbers[1].text, inputNumbers[2].text, inputNumbers[3].text, inputNumbers[4].text);
            GetComponent<KMBombModule>().HandlePass();
            moduleSolved = true;
            foreach (TextMesh answer in inputNumbers)
            {
                answer.color = answerColors[1];
            }
        }
        else
        {
            Debug.LogFormat("[Quintuples #{0}] Strike! You entered {1}{2}{3}{4}{5}. That is not correct.", moduleId, inputNumbers[0].text, inputNumbers[1].text, inputNumbers[2].text, inputNumbers[3].text, inputNumbers[4].text);
            GetComponent<KMBombModule>().HandleStrike();
            for (int i = 0; i <= 4; i++)
            {
                if (inputNumbers[i].text == answers[i].ToString())
                {
                    inputNumbers[i].color = answerColors[1];
                }
                else
                {
                    inputNumbers[i].color = answerColors[2];
                }
            }
        }
    }

    public void OnUpCycleButton(KMSelectable iterator)
    {
        iterator.AddInteractionPunch(0.5f);
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        if (moduleSolved)
        {
            return;
        }
        for (int i = 0; i <= 4; i++)
        {
            if (iterator == upCycleButtons[i])
            {
                displayedInputNumbers[i] = (displayedInputNumbers[i] + 1) % 10;
                inputNumbers[i].text = displayedInputNumbers[i].ToString();
                inputNumbers[i].color = answerColors[0];
            }
        }
    }

    public void OnDownCycleButton(KMSelectable iterator)
    {
        iterator.AddInteractionPunch(0.5f);
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        if (moduleSolved)
        {
            return;
        }
        for (int i = 0; i <= 4; i++)
        {
            if (iterator == downCycleButtons[i])
            {
                displayedInputNumbers[i] = (displayedInputNumbers[i] + 9) % 10;
                inputNumbers[i].text = displayedInputNumbers[i].ToString();
                inputNumbers[i].color = answerColors[0];
            }
        }
    }
#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} submit 82341 [submit this answer] | !{0} press 82341 [cycle each slot up this many times] | !{0} submit [submit answer as is]";
#pragma warning restore 414

    public KMSelectable[] ProcessTwitchCommand(string command)
    {
        var commands = command.ToLowerInvariant().Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

        // “submit” command without numbers
        if (command.Length == 1 && commands[0] == "submit")
            return new[] { submitButton };

        if (commands.Length != 2)
            return null;

        // All other commands must have 5 numbers
        var numbers = commands[1].Where(c => !char.IsWhiteSpace(c)).ToArray();
        if (numbers.Length != 5 || numbers.Any(ch => ch < '0' || ch > '9'))
            return null;

        // “press”: cycles the slots a number of times
        if (commands[0] == "press")
            return numbers.SelectMany((ch, ix) => Enumerable.Repeat(upCycleButtons[ix], ch - '0')).ToArray();

        // “submit”: submits a specific answer
        if (commands[0] == "submit")
            return numbers.SelectMany((ch, ix) => Enumerable.Repeat(upCycleButtons[ix], (ch - '0' + 10 - displayedInputNumbers[ix]) % 10)).Concat(new[] { submitButton }).ToArray();

        return null;
    }
}
