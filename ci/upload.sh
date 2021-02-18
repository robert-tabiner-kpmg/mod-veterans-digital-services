#!/bin/bash
set -e

echo "============== INSTALLING CLOUD FOUNDRY CLI CLIENT =============="
# https://github.com/cloudfoundry/cli/releases
# wget --max-redirect=1 --output-document=cf_cli_6.26.0.tgz "https://cli.run.pivotal.io/stable?release=linux64-binary&version=7.1.0&source=github-rel"
# wget --max-redirect=1 --output-document=cf_cli.tgz "https://cli.run.pivotal.io/stable?release=linux64-binary&source=github"
# wget --output-document=cf_cli.tgz https://packages.cloudfoundry.org/stable?release=linux64-binary&version=7.2.0&source=github-rel

mv ./ci/cf-cli.tgz ./cf_cli.tgz
gunzip cf_cli.tgz
tar -xvf cf_cli.tar

echo "============== LOGGING INTO CLOUD FOUNDRY =============="
./cf login -a $CF_API -s $CF_SPACE -o $CF_ORGANIZATION -u $CF_USERNAME -p $CF_PASSWORD

echo "============== PUSHING CF APP =============="
# bash ci/zdt.sh
# export CF_DOCKER_PASSWORD=$DOCKER_PASSWORD
./cf push green --docker-image modveterans/forms:dev --docker-username $DOCKER_USERNAME

echo "============== SET CF Environment Variables =============="
./cf set-env green Email__ApiKey $NOTIFY_API
./cf set-env green Email__AFCSTemplateId $AFCS_TEMPLATE_ID
./cf set-env green Email__AFCSUserTemplateId $AFCS_USER_TEMPLATE_ID
./cf set-env green Email__AFCSEmailRecipient toby@codesure.co.uk
./cf set-env green Redis__Uri $REDIS_URL

echo "============== BINDING REDIS AND S3 =============="
./cf create-service redis tiny-3.2 $REDIS_SERVICE_NAME
./cf create-service aws-s3-bucket tiny-3.2 $S3_SERVICE_NAME
./cf bind-service $APP_NAME $REDIS_SERVICE_NAME
./cf bind-service $APP_NAME $S3_SERVICE_NAME