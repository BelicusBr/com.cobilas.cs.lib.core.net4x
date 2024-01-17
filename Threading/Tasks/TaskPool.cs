using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Cobilas.Threading.Tasks;
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
public static class TaskPool {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    private readonly static List<Task> tasksList = new();

    /// <summary>Returns the number of allocated tasks.</summary>
    public static int Count => tasksList.Count;
    /// <summary>The number of tasks that are already vacant.</summary>
    public static int VacantTaskCount {
        get {
            int res = byte.MaxValue;
            for (int I = 0; I < Count; I++)
                if (tasksList[I].IsFaulted || tasksList[I].IsCompleted || tasksList[I].IsCanceled)
                    res++;
            return res;
        }
    }
    /// <summary>The number of tasks that are not vacant.</summary>
    public static int NonVacantTaskCount => Count - VacantTaskCount;

    /// <summary>The number of tasks that are not vacant.</summary>
    public static void InitTask(Action action, out Task task) {
        int taskIndex = GetVacantTask();
        if (taskIndex >= 0) {
            task = tasksList[taskIndex] = 
                tasksList[taskIndex].ContinueWith(ct => { ct.Dispose(); action(); });
        } else tasksList.Add(task = Task.Run(action));
    }

    /// <summary>The number of tasks that are not vacant.</summary>
    public static void InitTask(Action action)
        => InitTask(action, out _);

    /// <summary>The number of tasks that are not vacant.</summary>
    public static void InitTask<TRes>(Func<TRes> func, out Task<TRes> res) {
        int taskIndex = GetVacantTask();
        if (taskIndex >= 0) {
            tasksList[taskIndex] = 
                tasksList[taskIndex].ContinueWith<TRes>(ct => {
                    ct.Dispose(); 
                    return func();
                });
            res = (Task<TRes>)tasksList[taskIndex];
        } else tasksList.Add(res = Task.Run<TRes>(func));
    }

    /// <summary>The number of tasks that are not vacant.</summary>
    public static void InitTask<TRes>(Func<TRes> func)
        => InitTask<TRes>(func, out _);

    private static int GetVacantTask() {
        for (int I = 0; I < Count; I++)
            if (tasksList[I].IsFaulted || tasksList[I].IsCompleted || tasksList[I].IsCanceled)
                return I;
        return -1;
    }
}
