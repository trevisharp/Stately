/* Author:  Leonardo Trevisan Silio
 * Date:    06/04/2023
 */
namespace Stately;

/// <summary>
/// Represents a controller that watch and interact with a collection of states.
/// </summary>
public abstract class Watcher
{
    bool canUpdate = false;
    bool needUpdate = false;
    object lockObj = new object();

    /// <summary>
    /// Implements effetivines interaction with states.
    /// </summary>
    protected abstract void interact();

    /// <summary>
    /// Block watch system and interact with the objects waiting changes.
    /// </summary>
    public void Interact()
    {
        if (!needUpdate)
            return;
        
        if (!canUpdate)
            return;
        
        lock (lockObj)
        {
            canUpdate = false;
            needUpdate = false;
            
            interact();

            canUpdate = true;
        }
    }

    /// <summary>
    /// Add a state to watch.
    /// </summary>
    /// <param name="state">The state watched</param>
    public void Watch(State state)
        => state.watchers.Add(this);
    
    /// <summary>
    /// Remove a state to watch.
    /// </summary>
    /// <param name="state">The state watched</param>
    public void Unwatch(State state)
        => state.watchers.Remove(this);

    internal void OnWatchUpdate()
        => needUpdate = true;
}