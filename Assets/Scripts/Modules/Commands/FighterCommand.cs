using System;

namespace Modules.Commands
{
    public class FighterCommand
    {
        private readonly Action<FighterScript> _action;

        public FighterCommand(Action<FighterScript> action)
        {
            this._action = action;
        }
        
        public void Run(FighterScript ctx)
        {
            _action.Invoke(ctx);
        }
    }
}