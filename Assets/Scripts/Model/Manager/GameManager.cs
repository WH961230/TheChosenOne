using System;
using System.Collections.Generic;

public class GameManager : IManager {
    private Dictionary<System.Type, IFeature> featureDict = new Dictionary<Type, IFeature>();
    private List<IFeature> features = new List<IFeature>();
    public void Register<T>(Game game) where T : IFeature, new() {
        if (Find<T>() == -1) {
            IFeature instance = new T();
            featureDict.Add(typeof(T), instance);
            features.Add(instance);
            instance.Init(game);
        }
    }

    public void Register(IFeature feature, Game game) {
        if (Find(feature) == -1) {
            featureDict.Add(feature.GetType(), feature);
            features.Add(feature);
            feature.Init(game);
        }
    }

    public void Remove(IFeature feature) {
        int index = Find(feature);
        if (index != -1) {
            featureDict.Remove(feature.GetType());
            features.RemoveAt(index);
            feature.Clear();
        }
    }

    public void Remove<T>() where T : IFeature, new() {
        int index = Find<T>();
        if (index != -1) {
            IFeature instance = features[index];
            featureDict.Remove(typeof(T));
            features.RemoveAt(index);
            instance.Clear();
        }
    }

    public T Get<T>() where T : IFeature {
        if (featureDict.TryGetValue(typeof(T), out IFeature ret)) {
            return (T)ret;
        }

        return default(T);
    }

    private int Find(IFeature feature) {
        for (int i = 0; i < features.Count; i++) {
            if (feature == features[i]) {
                return i;
            }
        }

        return -1;
    }

    private int Find<T>() {
        System.Type type = typeof(T);
        int count = features.Count;
        for (int i = 0; i < count; i++) {
            if (type == features[i].GetType()) {
                return i;
            }
        }

        return -1;
    }

    public void Update() {
        for (int i = 0; i < features.Count; i++) {
            features[i].Update();
        }
    }
}