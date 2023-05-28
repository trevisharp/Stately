/* Author:  Leonardo Trevisan Silio
 * Date:    06/04/2023
 */
namespace Stately;

/// <summary>
/// A Watcher used to a property can be watch a sub state.
/// </summary>
public class SubStateWatcher : Watcher
{
    private State mainState;

    public SubStateWatcher(State mainState)
        => this.mainState = mainState;

    protected override void interact() { }

    public override void OnWatchUpdate()
        => mainState.OnChanged();
}