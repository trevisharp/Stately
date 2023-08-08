/* Author:  Leonardo Trevisan Silio
 * Date:    08/08/2023
 */
namespace Stately.Watches;

/// <summary>
/// A Watcher used to a property can be watch a sub state.
/// </summary>
public class SubStateWatcher : Watcher
{
    private State mainState;

    public SubStateWatcher(State mainState) : base(true)
        => this.mainState = mainState;

    protected override void act()
        => mainState.OnChanged();
}