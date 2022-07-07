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

    public void UnRegister(int id) {
        register.UnRegister(id);
    }

    public void Dispather(int id) {
        register.Dispather(id);
    }

    public void Dispather<T>(int id, T t) {
        register.Dispather(id, t);
    }
}

public class Act {
    public Delegate handler;

    public void Invoke() {
        ((Action)handler).Invoke();
    }

    public void Invoke<T>(T t) {
        ((Action<T>)handler).Invoke(t);
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
}