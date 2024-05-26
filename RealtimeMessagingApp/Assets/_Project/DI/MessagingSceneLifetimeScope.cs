using MessagingApp.Core;
using MessagingApp.Messages;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MessagingSceneLifetimeScope : LifetimeScope
{
	[SerializeField] private MessageSettingsSo messageSettings;
	[SerializeField] private GameSettingsSo gameSettings;
	protected override void Configure(IContainerBuilder builder)
    {
		builder.RegisterInstance(messageSettings);
		builder.RegisterInstance(gameSettings);
	}
}
