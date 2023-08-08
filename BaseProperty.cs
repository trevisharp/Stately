/* Author:  Leonardo Trevisan Silio
 * Date:    08/08/2023
 */
namespace Stately;

using Watches;

/// <summary>
/// A base class for all properties.
/// </summary>
public class BaseProperty
{
    private State state;

    protected void SetState(State state)
        => this.state = state;

    protected void UpdateState()
        => this.state.OnChanged();

    protected Watcher CreateSubWatcher()
        => new SubStateWatcher(this.state);
}