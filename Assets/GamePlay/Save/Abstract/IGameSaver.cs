using System.Collections.Generic;

namespace GamePlay.Save.Abstract
{
    public interface IGameSaver
    {
        void SaveTemporary(List<BoardStateBlock> boardState);
        void ForceSave(List<BoardStateBlock> boardState);
    }
}