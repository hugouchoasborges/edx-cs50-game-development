using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.states
{
    public enum FSMStateEvent
    {
        MENU_PLAY_TRIGGERED,
        MENU_RESUME_TRIGGERED,
        MENU_EXIT_TRIGGERED,

        GAME_PAUSE_TRIGGERED,

        GAME_ON_LOAD_COMPLETE,

        PLAYER_ON_DEATH,
        GAME_OVER,
        RESPAWN_PLAYER,
    }
}
