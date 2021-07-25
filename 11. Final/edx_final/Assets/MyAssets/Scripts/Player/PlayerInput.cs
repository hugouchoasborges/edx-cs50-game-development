using behaviors;

namespace player
{
    public class PlayerInput : SpaceshipInput
    {
        // ========================== Unity Update ============================

        private void Update()
        {
            if (_inputEnabled) HandleInput();
        }
    }
}