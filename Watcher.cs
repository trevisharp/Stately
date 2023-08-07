/* Author:  Leonardo Trevisan Silio
 * Date:    07/08/2023
 */
namespace Stately;

/// <summary>
/// Represents a controller that watch and interact with a collection of states.
/// </summary>
public abstract class Watcher
{
    bool canUpdate = true;
    object lockObj = new object();

    /// <summary>
    /// Implements effetivines interaction with states.
    /// </summary>
    protected abstract void act();

    /// <summary>
    /// Block watch system and interact with the objects waiting changes.
    /// </summary>
    public void Interact()
    {   
        if (!canUpdate)
            return;
        
        lock (lockObj)
        {
            canUpdate = false;
            
            act();

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

    public virtual void OnWatchUpdate()
        => Interact();
}