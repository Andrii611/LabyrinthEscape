using System;
using System.Collections.Generic;
using System.Text;

namespace LabyrinthEscape
{
    interface IInteractable
    {
        void Interact(Prisoner player);
        bool IsPassable();
        char GetSymbol();
    }
}
