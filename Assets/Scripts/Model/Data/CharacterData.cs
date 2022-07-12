using System.Collections.Generic;

public class CharacterData : Data {
    public bool IsMainCharacter;
    public bool IsJumping;
    public bool IsLanding = true;
    public List<int> MySceneItemIds;
}