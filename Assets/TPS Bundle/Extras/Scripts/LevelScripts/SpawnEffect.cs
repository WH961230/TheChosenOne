using UnityEngine;

// This class is created for the example scene. There is no support for this script.
public class SpawnEffect : MonoBehaviour {

	public Material dissolveMaterial;
    public float spawnEffectTime = 2;
	public AnimationCurve fadeIn;

	private SceneFadeInOut sceneFade;

	ParticleSystem ps;
    float timer = 0;
    Renderer _renderer, _weaponRenderer;

    int shaderProperty;

	private Transform weapon;

	void Start ()
    {
		sceneFade = GameObject.FindGameObjectWithTag("Finish").GetComponent<SceneFadeInOut>();
		GetComponent<SkinnedMeshRenderer>().material = dissolveMaterial;
        shaderProperty = Shader.PropertyToID("_cutoff");
        _renderer = GetComponent<Renderer>();
        ps = GetComponentInChildren <ParticleSystem>();

        var main = ps.main;
        main.duration = spawnEffectTime;

        ps.Play();

		foreach (Transform child in GetComponentInParent<Animator>().GetBoneTransform(HumanBodyBones.RightHand))
		{
			weapon = child.Find("muzzle");
			if (weapon != null && weapon.parent.gameObject.activeSelf)
			{
				Transform toDestroy = weapon;
				weapon = weapon.parent;
				Destroy(toDestroy.gameObject);
				_weaponRenderer = weapon.GetComponentInChildren<MeshRenderer>();
				Material[] dissolveMaterials = new Material[_weaponRenderer.materials.Length];
				for (int i = 0; i < dissolveMaterials.Length; i++)
				{
					dissolveMaterials[i] = dissolveMaterial;
				}
				_weaponRenderer.materials = dissolveMaterials;
				break;
			}
		}
	}
	
	void Update ()
    {
        if (timer < spawnEffectTime/2)
        {
            timer += Time.deltaTime;
        }
		else if(timer < spawnEffectTime)
		{
			timer += Time.deltaTime;
			sceneFade.EndScene(false);
		}
		else
		{
			sceneFade.EndScene(false);
		}

        _renderer.material.SetFloat(shaderProperty, fadeIn.Evaluate( Mathf.InverseLerp(0, spawnEffectTime, timer)));
		if(_weaponRenderer)
			_weaponRenderer.material.SetFloat(shaderProperty, fadeIn.Evaluate(Mathf.InverseLerp(0, spawnEffectTime, timer)));

	}
}
