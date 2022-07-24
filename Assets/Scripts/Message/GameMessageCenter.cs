using System;
using System.Collections.Generic;

public class GameMessageCenter {
    private GameMessageRegisger register = new GameMessageRegisger();

    public void Register(int id, Action action) {
        register.Register(id, action);
    }

    public void Register<T>(int id, Action<T> action) {
        register.Register(id, action);
    }

    public void Register<T1, T2>(int id, Action<T1, T2> action) {
        register.Register(id, action);
    }

    public void UnRegister(int id, Action a) {
        register.UnRegister(id, a);
    }

    public void UnRegister<T>(int id, Action<T> a) {
        register.UnRegister(id, a);
    }

    public void UnRegister<T1, T2>(int id, Action<T1, T2> a) {
        register.UnRegister(id, a);
    }

    public void Dispather(int id) {
        register.Dispather(id);
    }

    public void Dispather<T>(int id, T t) {
        register.Dispather(id, t);
    }

    public void Dispather<T1, T2>(int id, T1 t1, T2 t2) {
        register.Dispather(id, t1, t2);
    }
}

public class Act {
    public List<Delegate> handler = new List<Delegate>();

    public void Invoke() {
        foreach (var temp in handler) {
            ((Action) temp).Invoke();
        }
    }

    public void Invoke<T>(T t) {
        foreach (var temp in handler) {
            ((Action<T>) temp).Invoke(t);
        }
    }

    public void Invoke<T1, T2>(T1 t1, T2 t2) {
        foreach (var temp in handler) {
            ((Action<T1, T2>) temp).Invoke(t1, t2);
        }
    }
}

public class GameMessageRegisger {
    private Dictionary<int, Act> temps = new Dictionary<int, Act>();

    public void Register(int id, Delegate e) {
        if (temps.TryGetValue(id, out Act tmp)) {
            if (!tmp.handler.Contains(e)) {
                tmp.handler.Add(e);
            }

            temps[id] = tmp;
        } else {
            Act act = new Act();
            act.handler.Add(e);
            temps.Add(id, act);
        }
    }

    public void UnRegister(int id) {
        if (temps.TryGetValue(id, out var act)) {
            temps.Remove(id);
        }
    }

    public void UnRegister(int id, Action a) {
        if (temps.TryGetValue(id, out Act act)) {
            int index = -1;
            for (int i = 0; i < act.handler.Count; i++) {
                if (act.handler[i] == a) {
                    index = i;
                }
            }

            act.handler.RemoveAt(index);
            temps[id] = act;
        }
    }

    public void UnRegister<T>(int id, Action<T> a) {
        if (temps.TryGetValue(id, out Act act)) {
            int index = -1;
            for (int i = 0; i < act.handler.Count; i++) {
                if (act.handler[i] == a) {
                    index = i;
                }
            }

            act.handler.RemoveAt(index);
            temps[id] = act;
        }
    }

    public void UnRegister<T1, T2>(int id, Action<T1, T2> a) {
        if (temps.TryGetValue(id, out Act act)) {
            int index = -1;
            for (int i = 0; i < act.handler.Count; i++) {
                if (act.handler[i] == a) {
                    index = i;
                }
            }

            act.handler.RemoveAt(index);
            temps[id] = act;
        }
    }

    public void Dispather(int id) {
        if (temps.TryGetValue(id, out var tmp)) {
            tmp.Invoke();
        }
    }

    public void Dispather<T>(int id, T t) {
        if (temps.TryGetValue(id, out var tmp)) {
            tmp.Invoke(t);
        }
    }

    public void Dispather<T1, T2>(int id, T1 t1, T2 t2) {
        if (temps.TryGetValue(id, out var tmp)) {
            tmp.Invoke(t1, t2);
        }
    }
}