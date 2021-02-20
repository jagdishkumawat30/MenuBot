// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.11.1

using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MenuBot.Bots
{
    public class EchoBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var txt = turnContext.Activity.Text;

            if ("Raise Ticket".Equals(txt))
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Write your code logic here to raise a new ticket"), cancellationToken);
            }
            else if ("Show Ticket Status".Equals(txt))
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Write your code logic here to show ticket status"), cancellationToken);
            }
            else if ("Help".Equals(txt))
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Write your code logic here to show help"), cancellationToken);
            }
            else
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Not a valid option."), cancellationToken);
            }
            var cardAttachment = CreateAdaptiveCardAttachment();
            await turnContext.SendActivityAsync(MessageFactory.Attachment(cardAttachment), cancellationToken);

        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome! This is a Menu Item Bot. Select your option.";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                    var cardAttachment = CreateAdaptiveCardAttachment();
                    await turnContext.SendActivityAsync(MessageFactory.Attachment(cardAttachment), cancellationToken);
                }
            }
        }

        private static Attachment CreateAdaptiveCardAttachment()
        {
            string[] paths = { ".", "Resources", "menuItemCard.json" };
            var adaptiveCardJson = File.ReadAllText(Path.Combine(paths));
            
            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };
            return adaptiveCardAttachment;
        }
    }
}
