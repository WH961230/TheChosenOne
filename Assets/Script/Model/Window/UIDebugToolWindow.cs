using UnityEngine;

public class UIDebugToolWindow : Window {
    private UIDebugToolComponent ToolComp;

    public override void Init(Game game, GameObj gameObj) {
        base.Init(game, gameObj);
        var tempGO = (UIDebugToolGameObj)gameObj;
        ToolComp = tempGO.GetComp();

        ToolComp.MyDebugToolFunctionWin.SetActive(false);
        ToolComp.MyDebugToolCloseBtn.gameObject.SetActive(false);
        ToolComp.MyConsoleWin.SetActive(false);
        ToolComp.MyDebugToolSideBtnWin.SetActive(false);

        ToolComp.MyDebugToolBtn.onClick.AddListener(() => {
            ToolComp.MyDebugToolFunctionWin.SetActive(true);
            ToolComp.MyConsoleWin.SetActive(false);
            ToolComp.MyDebugToolSideBtnWin.SetActive(true);
            ToolComp.MyDebugToolCloseBtn.gameObject.SetActive(true);
        });

        ToolComp.MyDebugToolCloseBtn.onClick.AddListener(() => {
            ToolComp.MyDebugToolFunctionWin.SetActive(false);
            ToolComp.MyDebugToolCloseBtn.gameObject.SetActive(false);
            ToolComp.MyDebugToolSideBtnWin.SetActive(false);
        });

        ToolComp.MyDebugToolCreateCharacterBtn.onClick.AddListener(() => {
            MyGS.Get<CharacterSystem>().InstanceCharacter(false);
        });
        
        ToolComp.MyDebugToolConsoleSelectBtn.onClick.AddListener(() => {
            ToolComp.MyConsoleWin.SetActive(true);
            ToolComp.MyDebugToolFunctionWin.SetActive(false);
        });
        
        ToolComp.MyDebugToolFunctionSelectBtn.onClick.AddListener(() => {
            ToolComp.MyConsoleWin.SetActive(false);
            ToolComp.MyDebugToolFunctionWin.SetActive(true);
        });

        ToolComp.MyDebugToolCreateCubeAtGround.onClick.AddListener(() => {
            var characterPos =  MyGS.Get<CharacterSystem>().GetGO(GameData.MainCharacterId).GetComp().transform.position;
            var tempObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            tempObj.transform.position = GameData.GetGround(characterPos);
        });
    }
}