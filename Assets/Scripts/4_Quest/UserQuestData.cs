using System.Collections.Generic;
using System;

[Serializable]
public class UserQuestDataSet
{
    public List<UserQuestData> QuestDatas;
}

[Serializable]
public class UserQuestData
{
    public int id;
    public string mb_id;
    public string quest_id;
    public int cond_num;
    public int completed;

    public UserQuestData(int id, string mb_id, string quest_id, int cond_num, int completed)
    {
        this.id = id;
        this.mb_id = mb_id;
        this.quest_id = quest_id;
        this.cond_num = cond_num;
        this.completed = completed;
    }
}

