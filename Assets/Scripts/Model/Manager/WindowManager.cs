using System;
using System.Collections.Generic;

public class WindowManager {
    private Dictionary<System.Type, IWindow> windowDict = new Dictionary<Type, IWindow>();
    private List<IWindow> windows = new List<IWindow>();

    public void Register<T>(Game game) where T : IWindow, new() {
        if (Find<T>() == -1) {
            IWindow instance = new T();
            windowDict.Add(typeof(T), instance);
            windows.Add(instance);
            instance.Init(game);
        }
    }

    public void Register(IWindow window, Game game) {
        if (Find(window) == -1) {
            windowDict.Add(window.GetType(), window);
            windows.Add(window);
            window.Init(game);
        }
    }

    public void Remove<T>() where T : IWindow {
        int index = Find<T>();
        if (index != -1) {
            IWindow instance = windows[index];
            windowDict.Remove(typeof(T));
            windows.RemoveAt(index);
            instance.Clear();
        }
    }

    public void Remove(IWindow window) {
        int index = Find(window);
        if (index != -1) {
            windowDict.Remove(window.GetType());
            windows.RemoveAt(index);
            window.Clear();
        }
    }

    public void RemoveAll() {
        foreach (var w in windows) {
            w.Clear();
        }
        
        windows.Clear();
        windowDict.Clear();
    }

    private int Find<T>() where T : IWindow {
        System.Type type = typeof(T);
        int count = windows.Count;
        for (int i = 0; i < count; i++) {
            if (type == windows[i].GetType()) {
                return i;
            }
        }

        return -1;
    }

    private int Find(IWindow window) {
        int count = windows.Count;
        for (int i = 0; i < count; i++) {
            if (window == windows[i]) {
                return i;
            }
        }

        return -1;
    }

    public T Get<T>() where T : IWindow {
        if (windowDict.TryGetValue(typeof(T), out IWindow ret)) {
            return (T)ret;
        }

        return default;
    }

    public void Update() {
        for (int i = 0; i < windows.Count; i++) {
            windows[i].Update();
        }
    }
}