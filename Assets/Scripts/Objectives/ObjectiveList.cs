using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ObjectiveList
{
    public List<Objective> Objectives;
    public bool IsCompleted = false;
    public bool IsWinnable = true;
    public string ProgressSummary;

    private StringBuilder stringBuilder = new StringBuilder();

    public ObjectiveList(List<Objective> list)
    {
        Objectives = list;
    }

    public void EvaluateList()
    {
        int objectiveCount = Objectives.Count;
        int completedObjectives = 0;
        int winnableObjectives = objectiveCount;
        
        foreach (Objective objective in Objectives)
        {
            if (objective.EvaluateObjective())
            {
                completedObjectives++;
            }
            if (!objective.IsWinnable())
            {
                winnableObjectives--;
            }
            stringBuilder.AppendLine(objective.Progress);
        }
        if (completedObjectives == objectiveCount)
        {
            IsCompleted = true;
        }
        if (winnableObjectives < objectiveCount)
        {
            IsWinnable = false;
        }
        ProgressSummary = stringBuilder.ToString();
        stringBuilder.Length = 0;
    }


}
