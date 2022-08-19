using UnityEngine;

public class UIDebugToolWindow : Window {
    private UIDebugToolComponent comp;

    public override void Init(Game game, GameObj gameObj) {
        base.Init(game, gameObj);
        var tempGO = (UIDebugToolGameObj)gameObj;
        comp = tempGO.GetComp();

        comp.MyDebugToolFunctionWin.SetActive(false);
        comp.MyDebugToolCloseBtn.gameObject.SetActive(false);
        comp.MyConsoleWin.SetActive(false);
        comp.MyDebugToolSideBtnWin.SetActive(false);

        comp.MyDebugToolBtn.onClick.AddListener(() => {
            comp.MyDebugToolFunctionWin.SetActive(true);
            comp.MyConsoleWin.SetActive(false);
            comp.MyDebugToolSideBtnWin.SetActive(true);
            comp.MyDebugToolCloseBtn.gameObject.SetActive(true);
        });

        comp.MyDebugToolCloseBtn.onClick.AddListener(() => {
            comp.MyDebugToolFunctionWin.SetActive(false);
            comp.MyDebugToolCloseBtn.gameObject.SetActive(false);
            comp.MyDebugToolSideBtnWin.SetActive(false);
        });

        comp.MyDebugToolCreateCharacterBtn.onClick.AddListener(() => {
            MyGS.CharacterS.InstanceCharacter(false);
        });
        
        comp.MyDebugToolConsoleSelectBtn.onClick.AddListener(() => {
            comp.MyConsoleWin.SetActive(true);
            comp.MyDebugToolFunctionWin.SetActive(false);
        });
        
        comp.MyDebugToolFunctionSelectBtn.onClick.AddListener(() => {
            comp.MyConsoleWin.SetActive(false);
            comp.MyDebugToolFunctionWin.SetActive(true);
        });

        comp.MyDebugToolCreateCubeAtGround.onClick.AddListener(() => {
            var characterPos =  MyGS.CharacterS.GetGO(GameData.MainCharacterId).GetComp().transform.position;
            var tempObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            tempObj.transform.position = GameData.GetGround(characterPos);
        });
    }
}