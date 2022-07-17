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

    public void UnRegister(int id) {
        register.UnRegister(id);
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
    public Delegate handler;

    public void Invoke() {
        ((Action) handler).Invoke();
    }

    public void Invoke<T>(T t) {
        ((Action<T>) handler).Invoke(t);
    }

    public void Invoke<T1, T2>(T1 t1, T2 t2) {
        ((Action<T1, T2>) handler).Invoke(t1, t2);
    }
}

public class GameMessageRegisger {
    private Dictionary<int, Act> temps = new Dictionary<int, Act>();

    public void Register(int id, Delegate e) {
        if (!temps.TryGetValue(id, out var tmp)) {
            temps.Add(id, new Act() {
                handler = e,
            });
        }
    }

    public void UnRegister(int id) {
        if (temps.TryGetValue(id, out var tmp)) {
            temps.Remove(id);
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